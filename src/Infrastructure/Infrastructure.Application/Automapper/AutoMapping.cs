using AutoMapper;
using Domain.DbModels;
using Domain.DataTransferObjects;

namespace Infrastructure.Application.Automapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ToDoList, ToDoListDto>().ReverseMap();
        }
    }
}

