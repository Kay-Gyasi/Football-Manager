namespace Football_Manager.Users;

public class UsersController : Controller
{
    private readonly UserProcessor _processor;

    public UsersController(UserProcessor processor)
    {
        _processor = processor;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _processor.LoginAsync(command);
        return result.IsT0 ? Ok(result.AsT0) : BuildProblemDetails(result.AsT1);
    }
    
    [HttpPost("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> MakeAdmin([FromRoute] int id)
    {
        var success = await _processor.MakeAdmin(id);
        return success ? NoContent() : BadRequest();
    }
}