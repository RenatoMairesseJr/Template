using Microsoft.AspNetCore.Identity;

namespace Domain.DbModels.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
