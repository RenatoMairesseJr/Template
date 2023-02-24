using Domain.DataTransferObjects.Identity;

namespace Domain.Interfaces.Identity
{
    public interface IUserProvider
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<RegistrationResponse> Register(RegistrationRequest request);
        Task<List<string>> GetMenu(int userId);
    }
}
