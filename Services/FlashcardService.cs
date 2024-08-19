using Models;
using Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Services;
public class FlashcardService
{
    private readonly IConfiguration _config;
    private readonly IRepository _repo;
    public FlashcardService(IConfiguration configuration, IRepository repo)
    {
        _config = configuration;
        _repo = repo;
    }

    [HttpGet]
    public async Task<List<Flashcard>> GetFlashcards()
    {
        return await _repo.GetFlashcards();

    }

    [HttpGet]
    public async Task<Flashcard> GetFlashcard(int id)
    {
        return await _repo.GetFlashcardById(id);
    }

    [HttpPost]
    public async Task<Flashcard> AddFlashcard(Flashcard flashcard)
    {
        return await _repo.AddFlashcard(flashcard);
    }

    [HttpDelete]
    public async Task<Flashcard> DeleteFlashcard(int id)
    {
        return await _repo.DeleteFlashcard(id);
    }

    [HttpPut]
    public async Task<Flashcard> UpdateFlashcard(Flashcard flashcard)
    {

        return await _repo.UpdateFlashcard(flashcard);
    }


    [HttpGet]
    public async Task<List<Flashcard>> GetStudySetCards(int studySetId)
    {

        return await _repo.GetStudySetCards(studySetId);
    }




}