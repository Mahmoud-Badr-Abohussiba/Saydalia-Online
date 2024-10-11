using Saydalia_Online.Models;

namespace Saydalia_Online.Interfaces.InterfaceRepositories
{
    public interface ICategoryRepository : IGenaricRepository<Category>
    {
        Task<Category> GetByIdWithProducts(int id);
    }
}
