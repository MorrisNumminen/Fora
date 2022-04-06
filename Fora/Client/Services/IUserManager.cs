using Fora.Shared;

namespace Fora.Client.Services
{
    public interface IUserManager
    {
        Task RegisterUser(UserDto user);

    }
}