using BlogApp.Domain.Enums;

namespace BlogApp.Domain.Entities;

public class User : BaseEntity
{
    public string UserName { get; set; } = string.Empty;
    public int Age { get; set; } = 0;
    public string FullName { get; set; } = string.Empty;
    public UserType Role { get; set; } = UserType.Default;
}