using Fora.Shared;

namespace Fora.Client.Services
{
    public interface IUserManager
    {
        Task<string> RegisterUser(UserDto user);
        Task<string> LogInUser(UserDto userToLogin);
        Task<LoginDto> CheckUserLogin(string token);


    }
}