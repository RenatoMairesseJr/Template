using Domain.DbModels;

namespace Infrastructure.Repository.Interfaces
{
    public interface IToDoListRepository : IGenericRepository<ToDoList>
    {
        Task<List<ToDoList>> GetByUserId(int userId);
    }
}
