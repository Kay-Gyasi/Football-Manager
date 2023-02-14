namespace Football_Manager.Models;

// Not best way to handle this but keep for now 
public class UserModel
{
    public PlayerDto? Player { get; set; }
    public CoachDto? Coach { get; set; }
}

public class UserCommandModel
{
    public PlayerCommand? Player { get; set; }
    public CoachCommand? Coach { get; set; }
    public List<Lookup>? Teams { get; set; }
}

public class Lookup
{
    public int Id { get; set; }
    public int Name { get; set; }
}