namespace Domain.DataTransferObjects
{
    public class ToDoListDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? DateCreated { get; set; }
        public DateTime? DateCompleted { get; set; }
    }
}
