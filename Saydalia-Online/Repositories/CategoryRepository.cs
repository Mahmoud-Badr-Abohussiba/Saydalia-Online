using Microsoft.EntityFrameworkCore;
using Saydalia_Online.Interfaces.InterfaceRepositories;
using Saydalia_Online.Models;

namespace Saydalia_Online.Repositories
{
    public class CategoryRepository : GenaricRepository<Category>, ICategoryRepository
    {
        private readonly SaydaliaOnlineContext _dbContext;

        public CategoryRepository(SaydaliaOnlineContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> GetByIdWithProducts(int id)
        {
            return await _dbContext.categories.Where(e=>e.Id ==  id)?
                .Include(e=>e.Medicines)?
                .FirstOrDefaultAsync();
        }
    }
}
