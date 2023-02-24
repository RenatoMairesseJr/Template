using AutoMapper;
using Domain.DataTransferObjects;
using Domain.DbModels;
using Infrastructure.Repository.DatabaseConfig;
using Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Infrastructure.Repository
{
    public class ToDoListRepository : GenericRepository<ToDoList>, IToDoListRepository
    {
        public ToDoListRepository(DatabaseContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        private readonly IMapper _mapper;

        public async Task<List<ToDoList>> GetByUserId(int userId) => null;
    }
}
