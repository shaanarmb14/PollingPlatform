using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Auth;

namespace Legislation.Api.Law;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = Policies.LegislatorOnlyPolicy)]
public class LawsController(
    ILawRepository repository, 
    ILogger<LawsController> logger
) : ControllerBase
{
    [HttpGet]
    public IActionResult GetMany()
    {
        try
        {
            var laws = repository.GetMany();

            return Ok(laws);

        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching all laws");
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "An error occurred while fetching all laws."
            );
        }
    }

    [HttpGet("{id:int}")]
    public IActionResult GetByID(int id)
    {
        try
        {
            var law = repository.GetByID(id);

            if (law is null)
            {
                return NotFound();
            }

            return Ok(law);

        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching law with ID {LawID}", id);
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "An error occurred while fetching the law using the ID specified."
            );
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateLawRequest req)
    {
        try
        {
            var newLaw = repository.Create(req);

            return Ok(newLaw);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error creating law");
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "An error occurred creating the law"
            );
        }
    }

    [HttpPatch]
    public IActionResult Update([FromBody] UpdateLawRequest req)
    {
        try
        {
            var updatedLaw = repository.Update(req);

            return Ok(updatedLaw);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating law");
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "An error occurred updating the law"
            );
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        try
        {
            var deleted = repository.Delete(id);

            return deleted ? NoContent() : NotFound();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error deleting law with ID {LawID}", id);
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "An error occurred deleting the law using the provided ID"
            );
        }
    }
}
