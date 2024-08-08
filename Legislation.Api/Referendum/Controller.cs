using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Legislation.Api.Referendum;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = Auth.Roles.LegislatorRole)]
public class ReferendumsController(
    IReferendumRepository repository, 
    ILogger<ReferendumsController> logger
) : ControllerBase
{
    [HttpGet]
    public IActionResult GetMany()
    {
        try
        {
            var referendums = repository.GetMany();

            return Ok(referendums);

        } catch (Exception e)
        {
            logger.LogError(e, "Error fetching all referendums");
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "An error occurred while fetching all referendums."
            );
        }
    }

    [HttpGet("{id:int}")]
    public IActionResult GetByID(int id)
    {
        try
        {
            var referendum = repository.GetByID(id);

            if (referendum is null)
            {
                return NotFound();
            }

            return Ok(referendum);

        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching referendum with ID {ReferendumID}", id);
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "An error occurred while fetching the referendum using the ID specified."
            );
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateReferendumRequest request)
    {
        try
        {
            var newReferendum = repository.Create(request);

            return Ok(newReferendum);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error creating referendum");
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "An error occurred creating the referendum"
            );
        }
    }

    [HttpPut]
    public IActionResult Update([FromBody] UpdateReferendumRequest request)
    {
        if (request is null)
        {
            return BadRequest("No POST body found");
        }

        try
        {
            var updatedReferendum = repository.Update(request);

            return Ok(updatedReferendum);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating referendum");
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "An error occurred updating the referendum"
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
            logger.LogError(e, "Error deleting referendum with ID {ReferendumID}", id);
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "An error occurred deleting the referendum using the provided ID"
            );
        }
    }
}
