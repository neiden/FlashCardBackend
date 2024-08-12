using Models;
using Context;
using Microsoft.EntityFrameworkCore;

public class Repo : IRepository
{

    private readonly FlashcardContext _context;

    public Repo(FlashcardContext context)
    {
        _context = context;
    }

    public async Task<List<Flashcard>> GetFlashcards()
    {
        return await _context.Flashcards.ToListAsync();
    }

    public async Task<Flashcard> AddFlashcard(Flashcard flashcard)
    {
        _context.Flashcards.Add(flashcard);
        await _context.SaveChangesAsync();
        return flashcard;
    }

    public async Task<Flashcard> DeleteFlashcard(int id)
    {
        var flashcard = await _context.Flashcards.FindAsync(id);
        if (flashcard == null)
        {
            return null;
        }

        _context.Flashcards.Remove(flashcard);
        await _context.SaveChangesAsync();

        return flashcard;
    }

    public async Task<Flashcard> UpdateFlashcard(Flashcard flashcard)
    {

        _context.Entry(flashcard).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return flashcard;
    }
}