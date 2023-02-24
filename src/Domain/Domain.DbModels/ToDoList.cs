using Domain.DbModels.Common;

namespace Domain.DbModels;

public class ToDoList : EntityBase
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
    public DateTime? DateCompleted { get; set; }
}
