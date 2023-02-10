using Football_Manager.Users;

namespace Football_Manager.Coaches;

public class CoachCommand
{
    public int? Id { get; set; }
    public int UserId { get; set; }
    public int? TeamId { get; set; }
    public int YearsOfExperience { get; set; }
    public bool IsMain { get; set; }
    public UserCommand? User { get; set; }
}

public class CoachDto
{
    public int? Id { get; set; }
    public int UserId { get; set; }
    public int? TeamId { get; set; }
    public int YearsOfExperience { get; set; }
    public bool IsMain { get; set; }
    public UserDto? User { get; set; }
    public TeamDto? Team { get; set; }

    public static explicit operator CoachDto(Coach? coach)
    {
        if (coach is null) return null;
        return new()
        {
            Id = coach.Id,
            UserId = coach.UserId,
            TeamId = coach.TeamId,
            YearsOfExperience = coach.YearsOfExperience,
            IsMain = coach.IsMain,
            User = (UserDto) coach.User,
            Team = (TeamDto) coach.Team
        };
    }
}

public class CoachPageDto
{
    public int? Id { get; set; }
    public int UserId { get; set; }
    public int? TeamId { get; set; }
    public int YearsOfExperience { get; set; }
    public bool IsMain { get; set; }
    public UserPageDto? User { get; set; }
    public TeamPageDto? Team { get; set; }
    
    public static explicit operator CoachPageDto(Coach? coach)
    {
        if (coach is null) return null;
        return new()
        {
            Id = coach.Id,
            UserId = coach.UserId,
            TeamId = coach.TeamId,
            YearsOfExperience = coach.YearsOfExperience,
            IsMain = coach.IsMain,
            User = (UserPageDto) coach.User,
            Team = (TeamPageDto) coach.Team
        };
    }
    
    public static PaginatedList<CoachPageDto> ToPageDto(PaginatedList<Coach> paginated)
    {
        var pageDto = new List<CoachPageDto>();
        foreach (var player in paginated.Data)
        {
            pageDto.Add((CoachPageDto) player);
        }
        
        return new PaginatedList<CoachPageDto>(pageDto, paginated.TotalCount, paginated.CurrentPage, paginated.PageSize);
    }
}