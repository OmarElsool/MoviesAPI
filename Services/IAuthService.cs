using Movies.Models;

namespace Movies.Services
{
    public interface IAuthService
    {
        Task<AuthModel> Register(RegisterModel model);
        Task<AuthModel> Login(LoginModel model);
    }
}
