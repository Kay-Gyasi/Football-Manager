namespace Data;

public class User : IdentityUser<int>
{
    private readonly List<Player> _players = new ();
    public IReadOnlyList<Player> Players => _players.AsReadOnly();
    private readonly List<Coach> _coaches = new ();
    public IReadOnlyList<Coach> Coaches => _coaches.AsReadOnly();
}