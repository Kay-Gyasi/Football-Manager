using System.ComponentModel;

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

    [DisplayName("Team")]
    [Required]
    public int? TeamId { get; set; }

    [DisplayName("Jersey Name")]
    public string? JerseyName { get; set; }

    [DisplayName("Jersey Number")]
    [Required]
    public int? JerseyNumber { get; set; }

    [DisplayName("Primary Position")]
    [Required]
    public Position PrimaryPosition { get; set; }

    [DisplayName("Secondary Position")]
    public Position? SecondaryPosition { get; set; }

    [Required]
    public Nationality Nationality { get; set; }
    public UserCommand User { get; set; } = new();

    public static explicit operator PlayerCommand(PlayerDto? dto)
    {
        if (dto is null) return null!;
        return new PlayerCommand()
        {
            Id = dto.Id,
            UserId = dto.UserId,
            TeamId = dto.TeamId,
            JerseyName = dto.JerseyName,
            JerseyNumber = dto.JerseyNumber,
            PrimaryPosition = dto.PrimaryPosition,
            SecondaryPosition = dto.SecondaryPosition,
            Nationality = dto.Nationality,
            User = (UserCommand) dto.User!
        };
    }
}

public enum Position
{
    Goalkeeper = 1,
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