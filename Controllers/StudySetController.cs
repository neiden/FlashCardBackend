


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Serilog;
using Services;

[Authorize]
[ApiController]
[Route("[controller]")]
public class StudySetController : ControllerBase
{

    private readonly StudySetService _studyService;

    public StudySetController(StudySetService studySetService)
    {
        _studyService = studySetService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudySet(StudySet studySet)
    {
        Log.Information("Creating new study set: " + studySet);
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        try
        {
            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
            var jwtToken = authHeader.Replace("Bearer ", "");
            int id = int.Parse(JWTParser.GetClaim(jwtToken, "unique_name")!);
            studySet.UserId = id;
            var result = await _studyService.CreateStudySet(studySet);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal server error while getting study sets for user: " + e.Message);
        }


    }

    [HttpGet("userSets")]
    public async Task<IActionResult> GetUsersStudySets()
    {
        try
        {

            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
            // Extract the JWT token from the Authorization header
            var jwtToken = authHeader.Replace("Bearer ", "");
            int id = int.Parse(JWTParser.GetClaim(jwtToken, "unique_name")!);
            Log.Information("Attempting to retrieve study sets for user:  " + id);
            var studySets = await _studyService.GetAllStudySetsById(id);
            if (studySets == null)
            {
                return NotFound();
            }
            return Ok(studySets);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal server error while getting study sets for user: " + e.Message);
        }
    }

    [AllowAnonymous]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAllStudySetsById(int userId)
    {
        Log.Information("Getting study sets for user " + userId);
        try
        {
            var studySets = await _studyService.GetAllStudySetsById(userId);
            if (studySets == null)
            {
                return NotFound();
            }
            return Ok(studySets);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal server error while getting study sets: " + e.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStudySet(StudySet studySet)
    {
        Log.Information("Updating study set: " + studySet);
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        try
        {
            var result = await _studyService.UpdateStudySet(studySet);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal server error while updating study set: " + e.Message);
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudySet(int id)
    {
        Log.Information("Deleting study set with id: " + id);
        try
        {
            await _studyService.DeleteStudySet(id);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal server error while deleting study set: " + e.Message);
        }
    }
}