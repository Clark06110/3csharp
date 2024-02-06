using System.ComponentModel.DataAnnotations;

namespace _3CSharpProj.Entities.Models;

public class Product
{
    [Key]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Image { get; set; }
    public string? Price { get; set; }
    public Boolean? Available { get; set; }
    public DateTime Added_time { get; set; }
}