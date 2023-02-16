namespace Football_Manager.Models;

public class TeamModel
{
    public CoachDto? Coach { get; set; }
    public PaginatedList<PlayerPageDto>? Players { get; set; }
}

public class PaginatedList<T>
{
    public List<T> Data { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasPrevious { get; set; }
    public bool HasNext { get; set; }
    public int From { get; set; }
    public int To { get; set; }
    public int CurrentPage { get; set; }
}

public class PaginatedQuery
{
    public PaginatedQuery(int start, int currentPage, int pageSize)
    {
        Start = start;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }
    
    public int Skip { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int Start { get; set; }

    public static PaginatedQuery Build(int start = 0, int currentPage = 1, int pageSize = 10)
        => new PaginatedQuery(start, currentPage, pageSize);
}

public class PlayerPageDto
{
    public int Id { get; set; }
    public string? JerseyName { get; set; }
    public int? JerseyNumber { get; set; }
    public Position PrimaryPosition { get; set; }
    public Nationality Nationality { get; set; }
    public UserPageDto User { get; set; }
    public TeamPageDto Team { get; set; }
}

public class UserPageDto
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public UserType Type { get; set; }
}

public class TeamPageDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime EstablishmentDate { get; set; }
    public string? StadiumName { get; set; }
}