using Data.Base;
using Data.Teams;

namespace Data.Players;

public class Player : Entity<int>
{
    private Player(int userId, int? teamId)
    {
        UserId = userId;
        if (teamId is 0) teamId = null;
        TeamId = teamId;
    }

    public int UserId { get; private set; }
    public int? TeamId { get; private set; }
    public string? JerseyName { get; private set; }
    public int? JerseyNumber { get; private set; }
    public Position PrimaryPosition { get; private set; }
    public Position? SecondaryPosition { get; private set; }
    public Nationality Nationality { get; private set; }
    public User? User { get; private set; }
    public Team? Team { get; private set; }

    public static Player Create(int userId, int? teamId)
        => new (userId, teamId);

    public Player HasUserId(int userId)
    {
        UserId = userId;
        return this;
    }

    public Player HasJerseyName(string? name)
    {
        JerseyName = name;
        return this;
    }
    
    public Player HasJerseyNumber(int? number)
    {
        JerseyNumber = number;
        return this;
    }
    
    public Player HasPrimaryPosition(Position position)
    {
        PrimaryPosition = position;
        return this;
    }

    public Player HasSecondaryPosition(Position? position)
    {
        SecondaryPosition = position;
        return this;
    }

    public Player WithNationality(Nationality nationality)
    {
        Nationality = nationality;
        return this;
    }

    public Player ForUser(User? user)
    {
        User = user;
        return this;
    }
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