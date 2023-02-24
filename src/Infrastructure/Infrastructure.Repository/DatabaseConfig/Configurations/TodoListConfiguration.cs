using Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Repository.DatabaseConfig.Configurations
{
    public class TodoListConfiguration : IEntityTypeConfiguration<ToDoList>
    {
        public void Configure(EntityTypeBuilder<ToDoList> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasData(new ToDoList
            {
                Id = 1,
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                Description = "Description",
                DateCreated = DateTime.Now,
                DateCompleted = null
            },
            new ToDoList
            {
                Id = 2,
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                Description = "Description 2",
                DateCreated = DateTime.Now,
                DateCompleted = null
            }); 
        }
    }
}
