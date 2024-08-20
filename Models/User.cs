using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{

    [Key]
    public int Id { get; set; }
    public required string Username { get; set; }
    public string Password { get; set; }
    public string PasswordSalt { get; set; }

}