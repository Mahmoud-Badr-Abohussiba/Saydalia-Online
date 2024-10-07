using PagedList;

namespace Saydalia_Online.Interfaces.InterfaceRepositories
{
    public interface IGenaricRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<IPagedList<T>> GetAllPaginated(int page);

        Task<T> GetById(int id);
        Task<int> Add(T item);

        Task<int> Update(T item);

        Task<int> Delete(T item);
    }
}
