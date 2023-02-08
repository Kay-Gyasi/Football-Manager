namespace Data.Teams;

public class Team : Entity<int>
{
    private Team(string name)
    {
        Name = name;
    }
    
    public string Name { get; private set; }
    public DateTime EstablishmentDate { get; private set; }
    public string? StadiumName { get; private set; }
    private readonly List<Player> _players = new ();
    public IReadOnlyList<Player> Players => _players.AsReadOnly();
    private readonly List<Coach> _coaches = new ();
    public IReadOnlyList<Coach> Coaches => _coaches.AsReadOnly();

    public static Team Create(string name)
        => new (name);

    public Team HasName(string name)
    {
        Name = name;
        return this;
    }

    public Team WasEstablishedOn(DateTime dateTime)
    {
        EstablishmentDate = dateTime.Date;
        return this;
    }

    public Team HasStadium(string name)
    {
        StadiumName = name;
        return this;
    }
}