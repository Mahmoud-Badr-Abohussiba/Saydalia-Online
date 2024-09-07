﻿using Microsoft.EntityFrameworkCore;
using Saydalia_Online.InterfaceRepositories;
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
            return await _dbContext.Medicines.Include( m => m.Categories).FirstOrDefaultAsync(m => m.Id == id);
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
    }
}
