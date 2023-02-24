using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        //queries
        Task<List<T>> GetAll();
        Task<T> GetById(int id);


        //commands
        Task<T> Create(T entity, bool save = true, bool log = false);
        Task<T> Update(T entity, bool save = true, bool log = false);
        Task<T> Remove(T entity, bool save = true, bool log = false);
        Task<List<T>> CreateRange(List<T> entity, bool save = true, bool log = false);
        Task<List<T>> UpdateRange(List<T> entity, bool save = true, bool log = false);
        Task<List<T>> RemoveRange(List<T> entity, bool save = true, bool log = false);


        //
        Task Save(bool log = false);

    }
}
