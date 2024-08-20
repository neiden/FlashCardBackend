using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class StudySet
{
    [Key]
    public Guid Id { get; set; }
    [ForeignKey("User")]
    public int? UserId { get; set; }
    public required string Category { get; set; }

}