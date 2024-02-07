namespace ibayProj.Entities.Repositories
{
    public interface ICartRepository<TCart>
    {
        public TCart GetById(Guid id);
        
        public void Add(TCart entity);

        public void Update(TCart entity);
        
        public void SaveChanges();
        
        public Task SaveChangesAsync();
    }
}

