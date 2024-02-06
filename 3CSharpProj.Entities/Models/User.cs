using System.ComponentModel.DataAnnotations;

namespace _3CSharpProj.Entities.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? Pseudo { get; set; }
    public string? Password { get; set; }
}