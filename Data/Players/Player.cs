using Data.Teams;

namespace Data.Players;

public class Player : Entity<int>
{
    private Player(string name, int userId, int teamId)
    {
        UserId = userId;
        Name = name;
        TeamId = teamId;
    }

    public int UserId { get; private set; }
    public int TeamId { get; private set; }
    public string Name { get; private set; }    
    public DateTime DateOfBirth { get; private set; }
    public Position PrimaryPosition { get; private set; }
    public Position? SecondaryPosition { get; private set; }
    public Nationality Nationality { get; private set; }
    public User User { get; private set; }
    public Team Team { get; private set; }

    public static Player Create(string name, int userId, int teamId)
        => new (name, userId, teamId);

    public Player HasUserId(int userId)
    {
        UserId = userId;
        return this;
    }

    public Player IsNamed(string name)
    {
        Name = name;
        return this;
    }

    public Player WasBornOn(DateTime date)
    {
        DateOfBirth = date.Date;
        return this;
    }

    public Player HasPrimaryPosition(Position position)
    {
        PrimaryPosition = position;
        return this;
    }

    public Player HasSecondaryPosition(Position position)
    {
        SecondaryPosition = position;
        return this;
    }

    public Player WithNationality(Nationality nationality)
    {
        Nationality = nationality;
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