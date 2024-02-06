using Microsoft.EntityFrameworkCore;
using _3CSharpProj.Entities.Contexts;
using _3CSharpProj.Entities.Models;

namespace _3CSharpProj.Entities.Repositories;

public class UserRepository(IBayContext ctx): IBasicRepository<User>
{
    public User GetById(Guid id)
    {
        return ctx.Users.SingleOrDefault(t => t.Id == id);
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        return await ctx.Users.SingleOrDefaultAsync(t => t.Id == id);
    }

    public List<User> GetAll()
    {
        return ctx.Users!.ToList();
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await ctx.Users!.ToListAsync();
    }

    public void Add(User entity)
    {
        ctx.Users?.Add(entity);
    }

    public async Task AddAsync(User entity)
    {
        await ctx.Users.AddAsync(entity);
    }

    public void Update(User entity)
    {
        ctx.Users?.Update(entity);
    }

    public void DeleteById(Guid id)
    {
        ctx.Users?.Remove(GetById(id));
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