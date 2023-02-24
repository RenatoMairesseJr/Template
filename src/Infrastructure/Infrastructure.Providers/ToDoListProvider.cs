using AutoMapper;
using Domain.DataTransferObjects;
using Domain.DbModels;
using Domain.Interfaces;
using Infrastructure.Repository.Interfaces;
using Presentation.Middleware.Exceptions;

namespace Infrastructure.Providers
{
    public class ToDoListProvider : IToDoListProvider
    {
        private IToDoListRepository _todoListrepository;
        private IMapper _mapper;

        public ToDoListProvider(IToDoListRepository todoListrepository, IMapper mapper)
        {
            _todoListrepository = todoListrepository;
            _mapper = mapper;
        }

        public async Task<List<ToDoListDto>> GetAll() {
            var allToDoLists = await _todoListrepository.GetAll();
            return _mapper.Map<List<ToDoListDto>>(allToDoLists); 
        }

        public async Task<List<ToDoListDto>> GetByUserId(int userId)
        {
            var toDoListByUser = await _todoListrepository.GetByUserId(userId);
            return _mapper.Map<List<ToDoListDto>>(toDoListByUser);
        }

        public async Task<ToDoListDto> GetById(int id)
        {
            var todoItem = await _todoListrepository.GetByUserId(id);
            return _mapper.Map<ToDoListDto>(todoItem);
        }

        public async Task<ToDoListDto> AddItemToList(ToDoListDto item)
        {
            if(string.IsNullOrEmpty(item.Description))
                throw new NotFoundException("Description can't be empty", "");

            item.DateCreated= DateTime.Now;
            var toAdd = _mapper.Map<ToDoList>(item);
            toAdd = await _todoListrepository.Create(toAdd);

            item.Id = toAdd.Id;

            return item;
        }

        public async Task<ToDoListDto> UpdateDescription(int id, string description)
        {
            var item = await _todoListrepository.GetById(id);
            item.Description = description;
            item = await _todoListrepository.Update(item);

            return _mapper.Map<ToDoListDto>(item);
        }

        public async Task<ToDoListDto> CompleteTask(int id)
        {
            var item = await _todoListrepository.GetById(id);
            item.DateCompleted = DateTime.Now;
            item = await _todoListrepository.Update(item);

            return _mapper.Map<ToDoListDto>(item);
        }

        public async Task<ToDoListDto> ResetCompleteTask(int id)
        {
            var item = await _todoListrepository.GetById(id);
            item.DateCompleted = null;
            item = await _todoListrepository.Update(item);

            return _mapper.Map<ToDoListDto>(item);
        }

        public async Task<bool> DeleteTask(int id)
        {
            var item = await _todoListrepository.GetById(id);

            if (item == null)
                throw new NotFoundException("Task doesn't exists or it was already deleted", id);
            await _todoListrepository.Remove(item);

            return true;
        }


    }
}
