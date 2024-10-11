using Microsoft.EntityFrameworkCore;
using PagedList;
using Saydalia_Online.Interfaces.InterfaceRepositories;
using Saydalia_Online.Models;

namespace Saydalia_Online.Repositories
{
    public class GenaricRepository<T> : IGenaricRepository<T> where T : class
    {
        private readonly SaydaliaOnlineContext _dbContext;
        public GenaricRepository(SaydaliaOnlineContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Add(T item)
        {
            await _dbContext.Set<T>().AddAsync(item);
            return await _dbContext.SaveChangesAsync();
        }

      
        public async Task<int> Delete(T item)
        {
            _dbContext.Set<T>().Remove(item);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return  _dbContext.Set<T>().ToList();

        }

        public async Task<IPagedList<T>> GetAllPaginated(int page)
        {
            return _dbContext.Set<T>().ToPagedList(page, 12);

        }

        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }


        public async Task<int> Update(T item)
        {
            var existingEntity = _dbContext.ChangeTracker.Entries<T>()
                                                         .FirstOrDefault(e => e.Entity == item);

            if (existingEntity == null)
            {
                _dbContext.Set<T>().Update(item);
            }
            else
            {
                // The entity is already being tracked, so update its values.
                _dbContext.Entry(existingEntity.Entity).CurrentValues.SetValues(item);
            }

            return await _dbContext.SaveChangesAsync();

        }

       
    }
}
