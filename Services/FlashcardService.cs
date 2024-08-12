using Models;
using Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Services;
public class FlashcardService
{
    private readonly IConfiguration _config;
    private readonly Repo _repo;
    public FlashcardService(IConfiguration configuration, Repo repo)
    {
        _config = configuration;
        _repo = repo;
    }

    [HttpGet]
    public async Task<List<Flashcard>> GetFlashcards()
    {
        return _repo.GetFlashcards().Result;
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





}