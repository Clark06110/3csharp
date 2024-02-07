using Microsoft.EntityFrameworkCore;
using ibayProj.Entities.Contexts;
using ibayProj.Entities.Models;

namespace ibayProj.Entities.Repositories;

public class CartRepository(IBayContext ctx): ICartRepository<Cart>
{
    public Cart GetById(Guid id)
    {
        return ctx.Carts.SingleOrDefault(t => t.Id == id);
    }

    public void Add(Cart entity)
    {
        ctx.Carts?.Add(entity);
    }

    public void Update(Cart entity)
    {
        ctx.Carts?.Update(entity);
    }

    public void SaveChanges()
    {
        ctx.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await ctx.SaveChangesAsync();
    }
}