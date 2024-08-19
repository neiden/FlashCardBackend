
namespace Models;

public class Flashcard
{
    public required int Id { get; set; }
    public required string Question { get; set; }
    public required string Answer { get; set; }
    public int StudySetId { get; set; }

}