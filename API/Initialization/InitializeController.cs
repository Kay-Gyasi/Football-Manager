namespace Football_Manager.Initialization;

public class InitializeController : Controller
{
    private readonly InitializationProcessor _processor;

    public InitializeController(InitializationProcessor processor)
    {
        _processor = processor;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Startup()
    {
        await _processor.Startup();
        return NoContent();
    }
}