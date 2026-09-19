using AkkWebService.DTOs;
using AkkWebService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AkkWebService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CookieCuttersController : ControllerBase
{
    private readonly ICookieCutterService _service;

    public CookieCuttersController(ICookieCutterService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get all cookie cutters, ordered by name.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CookieCutterDto>>> GetAll()
    {
        var results = await _service.GetAllAsync();
        return Ok(results);
    }

    /// <summary>
    /// Get a single cookie cutter by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<CookieCutterDto>> GetById(long id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result is null) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Search cookie cutters by name (partial match).
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<CookieCutterDto>>> Search([FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Search term 'name' is required.");

        var results = await _service.SearchByNameAsync(name);
        return Ok(results);
    }

    /// <summary>
    /// Create a new cookie cutter. Requires authentication.
    /// </summary>
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<CookieCutterDto>> Create([FromBody] CreateCookieCutterDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.CookieCutterId }, created);
    }

    /// <summary>
    /// Update an existing cookie cutter. Requires authentication.
    /// </summary>
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<CookieCutterDto>> Update(long id, [FromBody] UpdateCookieCutterDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var updated = await _service.UpdateAsync(id, dto);
        if (updated is null) return NotFound();
        return Ok(updated);
    }

    /// <summary>
    /// Delete a cookie cutter by ID. Requires authentication.
    /// </summary>
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
