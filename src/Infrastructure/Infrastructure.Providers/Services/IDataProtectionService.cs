namespace Infrastructure.Providers.Services
{
    public interface IDataProtectionService
    {
        string Unprotect(string text);
    }
}