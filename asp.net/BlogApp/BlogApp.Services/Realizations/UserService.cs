using BlogApp.Domain.Entities;
using BlogApp.Repositories;
using BlogApp.Services.Exceptions;
using BlogApp.Services.Interfaces;

namespace BlogApp.Services.Realizations;

public class UserService(IUserRepository repository) : IUserService
{
	public Task<PagedResult<User>> GetAllAsync(int page = 1, int pageSize = 10) =>
		repository.GetAllAsync(page, pageSize);

	public Task<User?> GetByIdAsync(Guid id) => repository.GetByIdAsync(id);

	public async Task<User> CreateAsync(User user)
	{
		await ValidateAsync(user);
		user.UserName = user.UserName.Trim();
		user.FullName = user.FullName.Trim();
		var created = await repository.AddAsync(user);
		await repository.SaveChangesAsync();
		return created;
	}

	public async Task<User> UpdateAsync(Guid id, User user)
	{
		var current = await repository.GetByIdAsync(id)
			?? throw new EntityNotFoundException($"User with id {id} was not found.");

		await ValidateAsync(user, id);

		current.UserName = user.UserName.Trim();
		current.FullName = user.FullName.Trim();
		current.Age = user.Age;
		current.Role = user.Role;

		var updated = await repository.UpdateByIdAsync(id, current)
			?? throw new EntityNotFoundException($"User with id {id} was not found.");

		await repository.SaveChangesAsync();
		return updated;
	}

	public async Task DeleteAsync(Guid id)
	{
		var current = await repository.GetByIdAsync(id)
			?? throw new EntityNotFoundException($"User with id {id} was not found.");

		var deleted = await repository.DeleteAsync(current.Id);
		if (!deleted)
		{
			throw new EntityNotFoundException($"User with id {id} was not found.");
		}

		await repository.SaveChangesAsync();
	}

	private async Task ValidateAsync(User user, Guid? updatingId = null)
	{
		if (string.IsNullOrWhiteSpace(user.UserName))
		{
			throw new ValidationException("Username is required.");
		}

		if (string.IsNullOrWhiteSpace(user.FullName))
		{
			throw new ValidationException("Full name is required.");
		}

		if (user.Age <= 0)
		{
			throw new ValidationException("Age must be greater than zero.");
		}

		var normalizedUserName = user.UserName.Trim();
		if (await repository.ExistsByUserNameAsync(normalizedUserName, updatingId))
		{
			throw new ConflictException($"User with username '{normalizedUserName}' already exists.");
		}
	}

}