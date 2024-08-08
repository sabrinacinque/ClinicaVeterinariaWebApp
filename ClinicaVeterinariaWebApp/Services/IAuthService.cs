using ClinicaVeterinariaWebApp.Models;

namespace ClinicaVeterinariaWebApp.Services
{
    public interface IAuthService
    {
        ApplicationUser Login(string username, string password);
    }
}
