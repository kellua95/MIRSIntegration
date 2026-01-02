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
    public async Task<ActionResult<List<TEntity>>> GetAll()
    {
        try
        {
            var result = await _testManager.GetAllTestsAsync();
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<TEntity>> GetById(int id)
    {
        try
        {
            var result = await _testManager.GetTestsByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
    #endregion

    #region  Create
    [HttpPost("create")]
    public async Task<ActionResult<TEntity>> Create([FromBody] TEntity entity)
    {
        try
        {
            var result = await _testManager.CreateAsync(entity);

            if (result == null)
                return BadRequest("Create failed");

            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
      

    }
    #endregion

    #region Update
    [HttpPut("update")]
    public async Task<ActionResult<TEntity>> Update([FromBody] TEntity entity)
    {
        try
        {
            var created = await _testManager.UpdateAsync(entity);

            if (created == null)
                return NotFound();

            return Ok(created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
    #endregion

    #region Delete
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _testManager.DeleteAsync(id);

            if (!result)
                return NotFound();

            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
  
    }
    #endregion

}
