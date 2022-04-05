using Fora.Shared;
using System.Net.Http.Json;

namespace Fora.Client.Services
{
    public class UserManager : IUserManager
    {
        private readonly HttpClient _http;

        public UserManager(HttpClient http)
        {
            _http = http;
        }

        public async Task RegisterUser(UserDto userToRegister)
        {
            // Registrera användare i API:t

            var result = await _http.PostAsJsonAsync<UserDto>("api/users", userToRegister);


        }
    }
}
