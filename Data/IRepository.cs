using Models;

public interface IRepository
{

    Task<List<Flashcard>> GetFlashcards();
    Task<Flashcard> AddFlashcard(Flashcard flashcard);
    Task<Flashcard> DeleteFlashcard(int id);
    Task<Flashcard> UpdateFlashcard(Flashcard flashcard);

}