namespace Football_Manager.Base;

[ApiController]
[Route("api/[controller]/[action]")]
public class Controller : ControllerBase
{
    protected IActionResult BuildProblemDetails(Exception ex)
        => Problem(title: ex.Message, statusCode: ex.GetStatusCode());
    
    protected IActionResult BuildProblemDetails(object? id)
    {
        var ex = new InvalidIdException($"Entity with id: {id} does not exist");
        return Problem(title: ex.Message, statusCode: ex.GetStatusCode());
    }
}