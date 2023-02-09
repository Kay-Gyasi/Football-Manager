namespace Data.Coaches;

public class Coach : Entity<int>
{
    private Coach(int userId)
    {
        UserId = userId;
    }
    
    public int UserId { get; private set; }
    public int? TeamId { get; private set; }
    public int YearsOfExperience { get; private set; }
    public bool IsMain { get; private set; }
    public User? User { get; private set; }
    public Team? Team { get; private set; }

    public static Coach Create(int userId)
        => new (userId);

    public Coach Coaches(int? teamId)
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

    public Coach HasUserId(int userId)
    {
        UserId = userId;
        return this;
    }

    public Coach CoachesTeamWithId(int? teamId)
    {
        TeamId = teamId;
        return this;
    }
    
    public Coach ForUser(User? user)
    {
        User = user;
        return this;
    }
}