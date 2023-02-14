namespace Data;

public sealed class User : IdentityUser<int>
{
    private User(UserType type, string firstName, string lastName)
    {
        Type = type;
        FirstName = firstName;
        LastName = lastName;
        UserName = string.Join("", firstName[..3], lastName);
    }
    
    public UserType Type { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime? DateOfBirth { get; private set; }


    public static User Create(UserType type, string firstName, string lastName)
    {
        return new(type, firstName, lastName);
    }

    public User OfType(UserType type)
    {
        Type = type;
        return this;
    }

    public User WasBornOn(DateTime? date)
    {
        DateOfBirth = date?.Date;
        return this;
    }

    public User HasFirstName(string firstname)
    {
        FirstName = firstname;
        return this;
    }
    
    public User HasLastName(string lastname)
    {
        LastName = lastname;
        return this;
    }
    
    public User HasUserName(string username)
    {
        UserName = username;
        return this;
    }

    public User WithEmail(string? email)
    {
        Email = email;
        return this;
    }
    
    public User WithPhone(string? phone)
    {
        PhoneNumber = phone;
        return this;
    }
}

public enum UserType
{
    Player = 1,
    Coach
}

