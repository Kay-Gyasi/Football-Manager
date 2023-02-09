﻿namespace Football_Manager.Teams;

public class TeamsController : Controller
{
    private readonly TeamProcessor _processor;

    public TeamsController(TeamProcessor processor)
    {
        _processor = processor;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Save(TeamCommand command)
    {
        var result = await _processor.UpsertAsync(command);
        return result.IsT0 ? CreatedAtAction(nameof(Get), new { id = result.AsT0 }, result.Value)
            : BuildProblemDetails(command.Id);
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