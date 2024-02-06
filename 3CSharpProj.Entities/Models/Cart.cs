namespace _3CSharpProj.Entities.Models;

public class Cart
{
    public Guid Id { get; set; }
    public virtual User? User { get; set; }
    public virtual ICollection<Product>? Products { get; set; }
}