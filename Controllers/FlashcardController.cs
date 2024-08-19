

using Microsoft.AspNetCore.Mvc;
using Services;
using Models;
using Serilog;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("[controller]")]
public class FlashcardController : ControllerBase
{
    private readonly FlashcardService _flashcardService;

    public FlashcardController(FlashcardService flashcardService)
    {
        _flashcardService = flashcardService;

    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetFlashcards()
    {
        Log.Information("GetFlashcards Controller called");
        return Ok(await _flashcardService.GetFlashcards());
    }


    [HttpGet("{id}")]
    public async Task<Flashcard> GetFlashcard(int id)
    {

        return await _flashcardService.GetFlashcard(id);
    }

    [HttpPost("create")]
    public async Task<IActionResult> AddFlashcard(Flashcard flashcard)
    {
        var result = await _flashcardService.AddFlashcard(flashcard);
        if (result == null)
        {
            return BadRequest();
        }

        return Ok(flashcard);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteFlashcard(int id)
    {
        var result = await _flashcardService.DeleteFlashcard(id);
        if (result == null)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateFlashcard(Flashcard flashcard)
    {
        var result = await _flashcardService.UpdateFlashcard(flashcard);
        if (result == null)
        {
            return BadRequest();
        }

        return Ok(flashcard);
    }


    [HttpGet("studySet/{studySetId}")]
    public async Task<List<Flashcard>> GetStudySetFlashcards(int studySetId)
    {
        Log.Information("Trying to get flashcards for study set " + studySetId);
        var result = await _flashcardService.GetStudySetCards(studySetId);
        return result;
    }




}