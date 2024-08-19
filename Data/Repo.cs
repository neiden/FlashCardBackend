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


    public async Task<Flashcard> GetFlashcardById(int id)
    {
        var card = await _context.Flashcards.FindAsync(id);
        return card;
    }



    public async Task<User> GetUserByLogin(string login)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == login);
        return user;
    }

    public async Task<User> CreateUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteUser(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateUser(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<User> GetUserById(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        await _context.SaveChangesAsync();
        return user;
    }



    public async Task<StudySet> CreateStudySet(StudySet studySet)
    {
        _context.StudySets.Add(studySet);
        await _context.SaveChangesAsync();
        return studySet;
    }

    public async Task<List<StudySet>> GetAllStudySetsById(int userId)
    {
        var studySets = await _context.StudySets.Where(s => s.UserId == userId).ToListAsync();
        return studySets;
    }

    public async Task<List<Flashcard>> GetStudySetCards(int studySetId)
    {
        var flashcards = await _context.Flashcards.Where(f => f.StudySetId == studySetId).ToListAsync();
        return flashcards;
    }

    public async Task<StudySet> UpdateStudySet(StudySet studySet)
    {
        _context.Entry(studySet).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return studySet;
    }

    public async Task DeleteStudySet(int id)
    {
        var studySet = await _context.StudySets.FirstOrDefaultAsync(s => s.Id == id);
        if (studySet != null)
        {
            _context.StudySets.Remove(studySet);
            await _context.SaveChangesAsync();
        }
    }

}