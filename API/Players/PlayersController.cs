namespace Football_Manager.Players;

public class PlayersController : Controller
{
    private readonly PlayerProcessor _processor;

    public PlayersController(PlayerProcessor processor)
    {
        _processor = processor;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Save(PlayerCommand command)
    {
        var result = await _processor.UpsertAsync(command);
        if (result.IsT1) return BuildProblemDetails(command.Id);
        return result.IsT2 ? BuildProblemDetails(result.AsT2) 
            : CreatedAtAction(nameof(Get), new { id = result.AsT0 }, result.Value);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(int id)
    {
        var dto = await _processor.GetAsync(id);
        return dto is null ? NoContent() : Ok(dto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        await _processor.DeleteAsync(id);
        return NoContent();
    }
}