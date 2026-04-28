using BlogApp.Services.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.API.Extensions;

public static class EndpointRequestExtensions
{
	public static Dictionary<string, string[]>? ValidateRequest<TRequest>(this TRequest request)
	{
		var validationContext = new ValidationContext(request!);
		var validationResults = new List<ValidationResult>();

		if (Validator.TryValidateObject(request!, validationContext, validationResults, true))
		{
			return null;
		}

		return validationResults
			.GroupBy(x => x.MemberNames.FirstOrDefault() ?? "request")
			.ToDictionary(
				x => x.Key,
				x => x.Select(v => v.ErrorMessage ?? "Validation failed.").ToArray());
	}

	public static IResult ToHttpResult(this ServiceException exception)
	{
		return exception switch
		{
			Services.Exceptions.ValidationException => Results.BadRequest(new { error = exception.Message }),
			EntityNotFoundException => Results.NotFound(new { error = exception.Message }),
			ConflictException => Results.Conflict(new { error = exception.Message }),
			_ => Results.Problem(title: "Service error", detail: exception.Message)
		};
	}
}
