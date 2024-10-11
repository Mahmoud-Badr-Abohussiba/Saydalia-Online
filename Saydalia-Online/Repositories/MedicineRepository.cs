using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PagedList;
using Saydalia_Online.Interfaces.InterfaceRepositories;
using Saydalia_Online.Models;

namespace Saydalia_Online.Repositories
{
    public class MedicineRepository : GenaricRepository<Medicine>, IMedicineRepository
    {
        private readonly SaydaliaOnlineContext _dbContext;

        public MedicineRepository(SaydaliaOnlineContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Medicine> Details(int id)
        {
            return await _dbContext.Medicines.Include( m => m.Categories).Include(m => m.OrderItems).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Medicine>> DisplayAllBetweenTwoPrices(int minPrice, int maxPrice)
        {
            return await _dbContext.Medicines.Where(m => m.Price >= minPrice && m.Price <= maxPrice).ToListAsync();
        }

        public async Task<IEnumerable<Medicine>> DisplayUsingNameFromAToZ()
        {
            return await _dbContext.Medicines.OrderBy(m => m.Name).ToListAsync();
        }

        public async Task<IEnumerable<Medicine>> DisplayUsingNameFromZToA()
        {
            return await _dbContext.Medicines.OrderByDescending(m => m.Name).ToListAsync();
        }

        public async Task<IEnumerable<Medicine>> DisplayUsingPriceHighToLow()
        {
            return await _dbContext.Medicines.OrderBy(m => m.Price).ToListAsync();
        }

        public async Task<IEnumerable<Medicine>> DisplayUsingPriceLowToHigh()
        {
            return await _dbContext.Medicines.OrderByDescending(m => m.Price).ToListAsync();
        }

        public async Task<Medicine> GetByIdAsNoTracking(int id)
        {
            var medicine = await _dbContext.Medicines
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(m => m.Id == id);

            // Check if the medicine was found, if not, throw an exception.
            if (medicine == null)
            {
                throw new KeyNotFoundException($"Medicine with ID {id} was not found.");
            }

            return medicine;
        }

        public async Task<IPagedList<Medicine>> SearchByName(string name)
        {
            return  _dbContext.Medicines.Where(m => m.Name.Contains(name)).ToPagedList(1,12);
        }

        public async Task<IPagedList<Medicine>> GetAllForCategory(int id,int page)
        {
            return _dbContext.Medicines.Where(m => m.Cat_Id == id).Include(m=>m.Categories).ToPagedList(page, 12);
        }
    }
}
