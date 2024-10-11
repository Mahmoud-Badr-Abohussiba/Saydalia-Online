using PagedList;
using Saydalia_Online.Models;

namespace Saydalia_Online.Interfaces.InterfaceRepositories
{
    public interface IMedicineRepository : IGenaricRepository<Medicine>
    {
        Task<Medicine> Details(int id);
        Task<IEnumerable<Medicine>> DisplayUsingNameFromAToZ();
        Task<IEnumerable<Medicine>> DisplayUsingNameFromZToA();
        Task<IEnumerable<Medicine>> DisplayUsingPriceLowToHigh();
        Task<IEnumerable<Medicine>> DisplayUsingPriceHighToLow();
        Task<IEnumerable<Medicine>> DisplayAllBetweenTwoPrices(int minPrice, int maxPrice);
        Task<IPagedList<Medicine>> SearchByName(string name);
        Task<IPagedList<Medicine>> GetAllForCategory(int id, int page);
        Task<Medicine> GetByIdAsNoTracking(int id);

    }
}
