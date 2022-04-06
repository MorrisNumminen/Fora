using Fora.Shared;

namespace Fora.Client.Services
{
    public interface IUserManager
    {
        Task LogInUser(UserDto userToLogin);
        Task RegisterUser(UserDto user);
    }
}