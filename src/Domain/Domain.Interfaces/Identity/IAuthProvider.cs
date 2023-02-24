using Domain.DataTransferObjects.Identity;

namespace Domain.Interfaces.Identity
{
    public interface IAuthProvider
    {
        Task<AuthResponse> Login(string credentials);
        Task<RegistrationResponse> Register(string credentials);
    }
}
