namespace Football_Manager.Users;

public class UserCommand
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string LastName { get; set; }
    public string? Password { get; set; }
}

public class UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public UserType Type { get; set; }

    public static explicit operator UserDto(User? user)
    {
        if (user is null) return null;
        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Type = user.Type
        };
    }
}

public class UserPageDto
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public UserType Type { get; set; }
    
    public static explicit operator UserPageDto(User user)
    {
        return new UserPageDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Type = user.Type
        };
    }
}