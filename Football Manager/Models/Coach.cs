namespace Football_Manager.Models;

public class CoachDto
{
    public int? Id { get; set; }
    public int UserId { get; set; }
    public int? TeamId { get; set; }
    public int YearsOfExperience { get; set; }
    public bool IsMain { get; set; }
    public UserDto? User { get; set; }
    public TeamDto? Team { get; set; }
}