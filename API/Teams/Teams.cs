using Data.Coaches;
using Data.Players;
using Data.Teams;
using Football_Manager.Players;

namespace Football_Manager.Teams;

public class TeamCommand
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public DateTime EstablishmentDate { get; set; }
    public string? StadiumName { get; set; }
}

public class TeamDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime EstablishmentDate { get; set; }
    public string? StadiumName { get; set; }
    public IReadOnlyList<Player>? Players { get; set; }
    public IReadOnlyList<Coach>? Coaches { get; set; }

    public static explicit operator TeamDto(Team? team)
    {
        if (team is null) return null;
        return new TeamDto
        {
            Id = team.Id,
            Name = team.Name,
            StadiumName = team.StadiumName,
            Players = team.Players,
            Coaches = team.Coaches
        };
    }
}

public class TeamPageDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime EstablishmentDate { get; set; }
    public string? StadiumName { get; set; }
    
    public static explicit operator TeamPageDto(Team? team)
    {
        if (team is null) return null;
        return new TeamPageDto
        {
            Id = team.Id,
            Name = team.Name,
            StadiumName = team.StadiumName,
            EstablishmentDate = team.EstablishmentDate
        };
    }
    
    public static PaginatedList<TeamPageDto> ToPageDto(PaginatedList<Team> paginated)
    {
        var pageDto = new List<TeamPageDto>();
        foreach (var player in paginated.Data)
        {
            pageDto.Add((TeamPageDto) player);
        }
        
        return new PaginatedList<TeamPageDto>(pageDto, paginated.TotalCount, paginated.CurrentPage, paginated.PageSize);
    }
}