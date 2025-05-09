

using Microsoft.AspNetCore.Mvc;
using TavernSystem.Application;
using TavernSystem.Models;

namespace TavernSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdventurersController : ControllerBase
{
    private readonly IAdventurerService _service;

    public AdventurersController(IAdventurerService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var adventurers = await _service.GetAllAsync();
        return Ok(adventurers.Select(a => new { a.Id, a.Nickname }));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var adventurer = await _service.GetByIdAsync(id);
        if (adventurer == null) return NotFound();
        return Ok(adventurer);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Adventurer adventurer)
    {
        try
        {
            await _service.CreateAsync(adventurer);
            return CreatedAtAction(nameof(Get), new { id = adventurer.Id }, adventurer);
        }
        catch (UnauthorizedAccessException ex)
        {
            return StatusCode(403, ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
//