using Domain.DataTransferObjects;

namespace Domain.Interfaces
{
    public interface IToDoListProvider
    {
        Task<List<ToDoListDto>> GetAll();
        Task<List<ToDoListDto>> GetByUserId(int userId);
        Task<ToDoListDto> GetById(int id);
        Task<ToDoListDto> AddItemToList(ToDoListDto item);
        Task<ToDoListDto> CompleteTask(int id);
        Task<ToDoListDto> ResetCompleteTask(int id);
        Task<ToDoListDto> UpdateDescription(int id, string description);
        Task<bool> DeleteTask(int id);
    }
}
