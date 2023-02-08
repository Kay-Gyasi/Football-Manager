using Data.Teams;

namespace Data.Coaches;

public class Coach : Entity<int>
{
    private Coach(int userId, string name)
    {
        UserId = userId;
        Name = name;
    }
    
    public int UserId { get; private set; }
    public int? TeamId { get; private set; }
    public string Name { get; private set; }
    public int YearsOfExperience { get; private set; }
    public bool IsMain { get; private set; }
    public User User { get; private set; }
    public Team Team { get; private set; }

    public static Coach Create(int userId, string name)
        => new (userId, name);

    public Coach IsNamed(string name)
    {
        Name = name;
        return this;
    }

    public Coach Coaches(int teamId)
    {
        TeamId = teamId;
        return this;
    }

    public Coach WithExperienceInYears(int years)
    {
        YearsOfExperience = years;
        return this;
    }

    public Coach IsMainCoach()
    {
        IsMain = true;
        return this;
    }
}