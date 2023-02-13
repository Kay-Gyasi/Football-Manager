namespace Football_Manager.Models;

public class PlayerDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int? TeamId { get; set; }
    public string? JerseyName { get; set; }
    public int? JerseyNumber { get; set; }
    public Position PrimaryPosition { get; set; }
    public Position? SecondaryPosition { get; set; }
    public Nationality Nationality { get; set; }
    public UserDto? User { get; set; }
    public TeamDto? Team { get; set; }
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
}

public class TeamDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime EstablishmentDate { get; set; }
    public string? StadiumName { get; set; }
}

public enum UserType
{
    Player = 1,
    Coach
}















public class PlayerCommand
{
    public int? Id { get; set; }
    public int UserId { get; set; }
    public int? TeamId { get; set; }
    public string? JerseyName { get; set; }
    public int? JerseyNumber { get; set; }
    public Position PrimaryPosition { get; set; }
    public Position? SecondaryPosition { get; set; }
    public Nationality Nationality { get; set; }
    public UserCommand User { get; set; } = new UserCommand();
}

public enum Position
{
    GoalKeeper = 1,
    Defender,
    Midfielder,
    Forward
}

public enum Nationality
{
    Ghanaian,
    Swiss,
    Nigerian,
    Argentinian,
    English
}