using Fora.Shared;

namespace Fora.Client.Services
{
    public interface IUserManager
    {
        void RegisterUser(UserDto user);
    }
}