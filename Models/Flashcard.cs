
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class Flashcard
{
    [Key]
    public required int Id { get; set; }
    public required string Question { get; set; }
    public required string Answer { get; set; }
    [ForeignKey("StudySet")]
    public Guid? StudySetId { get; set; }

}