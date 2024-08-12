

using Microsoft.AspNetCore.Mvc;
using Services;
using Models;

[ApiController]
[Route("[controller]")]
public class FlashcardController : ControllerBase
{
    private readonly FlashcardService _flashcardService;

    public FlashcardController(FlashcardService flashcardService)
    {
        _flashcardService = flashcardService;
    }

    [HttpGet]
    public async Task<List<Flashcard>> GetFlashcards()
    {
        return await _flashcardService.GetFlashcards();
    }

    [HttpPost("create")]
    public async Task<Flashcard> AddFlashcard(Flashcard flashcard)
    {
        return await _flashcardService.AddFlashcard(flashcard);
    }

    [HttpDelete("delete/{id}")]
    public async Task<Flashcard> DeleteFlashcard(int id)
    {
        return await _flashcardService.DeleteFlashcard(id);
    }

    [HttpPut("update")]
    public async Task<Flashcard> UpdateFlashcard(Flashcard flashcard)
    {
        return await _flashcardService.UpdateFlashcard(flashcard);
    }




}