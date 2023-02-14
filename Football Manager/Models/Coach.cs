namespace Football_Manager.Models;

public class CoachDto
{
    public int? Id { get; set; }
    public int UserId { get; set; }
    public int? TeamId { get; set; }
    public int YearsOfExperience { get; set; }
    public bool IsMain { get; set; }
    public UserDto? User { get; set; }
    public TeamDto? Team { get; set; }
}

public class CoachCommand
{
    public int? Id { get; set; }
    public int UserId { get; set; }
    public int? TeamId { get; set; }
    public int YearsOfExperience { get; set; }
    public bool IsMain { get; set; }
    public UserCommand? User { get; set; }

    public static explicit operator CoachCommand(CoachDto? dto)
    {
        if (dto is null) return null!;
        return new CoachCommand()
        {
            Id = dto.Id,
            UserId = dto.UserId,
            TeamId = dto.TeamId,
            YearsOfExperience = dto.YearsOfExperience,
            IsMain = dto.IsMain,
            User = (UserCommand) dto.User!
        };
    }
}