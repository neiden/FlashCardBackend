using Models;

public interface IRepository
{

    //Flashcard
    Task<List<Flashcard>> GetFlashcards();
    Task<Flashcard> GetFlashcardById(int id);
    Task<List<Flashcard>> GetStudySetCards(int studySetId);
    Task<Flashcard> AddFlashcard(Flashcard flashcard);
    Task<Flashcard> DeleteFlashcard(int id);
    Task<Flashcard> UpdateFlashcard(Flashcard flashcard);


    // User 
    Task<User> GetUserByLogin(string login);
    Task<User> CreateUser(User user);
    Task UpdateUser(User user);
    Task DeleteUser(int id);
    Task<User> GetUserById(int id);


    //Study Set
    Task<StudySet> CreateStudySet(StudySet studySet);
    Task<List<StudySet>> GetAllStudySetsById(int id);
    Task<StudySet> UpdateStudySet(StudySet studySet);
    Task DeleteStudySet(int id);

}