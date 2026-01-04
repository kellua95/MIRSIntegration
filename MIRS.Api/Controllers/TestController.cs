using Microsoft.AspNetCore.Mvc;
using MIRS.Domain.Interfaces.DomainServices;
using MIRS.Domain.Models;

namespace MIRS.Api.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class TestController<TEntity> : ControllerBase
    where TEntity : Test
{
    private readonly ITestManager<TEntity> _testManager;

    protected TestController(ITestManager<TEntity> testManager)
    {
        _testManager = testManager;
    }

    #region Get

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TEntity>>> GetAll()
    {
        var result = await _testManager.GetAllTestsAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TEntity>> GetById(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid ID");

        var entity = await _testManager.GetTestByIdAsync(id);

        if (entity == null)
            return NotFound();

        return Ok(entity);
    }

    #endregion

    #region Create

    [HttpPost]
    public async Task<ActionResult<TEntity>> Create([FromBody] TEntity entity)
    {
        if (entity == null)
            return BadRequest("Entity cannot be null");

        var created = await _testManager.CreateAsync(entity);

        if (created == null)
            return BadRequest("Create operation failed");

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    #endregion

    #region Update

    [HttpPut("{id:int}")]
    public async Task<ActionResult<TEntity>> Update(int id, [FromBody] TEntity entity)
    {
        if (entity == null || entity.Id != id)
            return BadRequest("Invalid entity or ID mismatch");

        var updated = await _testManager.UpdateAsync(entity);

        if (updated == null)
            return NotFound();

        return Ok(updated);
    }

    #endregion

    #region Delete

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid ID");

        var deleted = await _testManager.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

    #endregion
}
