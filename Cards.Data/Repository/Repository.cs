using Cards.Data.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Cards.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        public Repository(ApplicationDbContext dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<T> AddAsync(T entity)
        {

            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            using (var dbContextTransaction = await _context.Database.BeginTransactionAsync())
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();

                await dbContextTransaction.CommitAsync();
            }
            return true;
        }


        public async Task<T> UpdateAsync(Guid Id, T entity)
        {
            var dbEntity = await _context.FindAsync<T>(Id);

            _context.Entry(dbEntity).CurrentValues.SetValues(entity);

            _context.Entry(dbEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return entity;
        }

    }
}
