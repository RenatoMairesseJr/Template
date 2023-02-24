namespace Domain.DataTransferObjects.Identity
{
    public class MenuOptions
    {
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Component { get; set; } = string.Empty;
        public bool ShowInMenu { get; set; }
        public List<string>? permissions { get; set; }
    }
}
