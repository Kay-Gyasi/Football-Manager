namespace Football_Manager.Players;

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
    public UserCommand? User { get; set; }
}

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

    public static explicit operator PlayerDto(Player? player)
    {
        if (player is null) return default!;
        return new PlayerDto
        {
            Id = player.Id,
            UserId = player.UserId,
            TeamId = player.TeamId,
            JerseyName = player.JerseyName,
            JerseyNumber = player.JerseyNumber,
            PrimaryPosition = player.PrimaryPosition,
            SecondaryPosition = player.SecondaryPosition,
            Nationality = player.Nationality,
            User = (UserDto) player.User,
            Team = (TeamDto) player.Team
        };
    }
}

public class PlayerPageDto
{
    public int Id { get; set; }
    public string? JerseyName { get; set; }
    public int? JerseyNumber { get; set; }
    public Position PrimaryPosition { get; set; }
    public Nationality Nationality { get; set; }
    public UserPageDto User { get; set; } // pick only info needed
    public TeamPageDto Team { get; set; }
}