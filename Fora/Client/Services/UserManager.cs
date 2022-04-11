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

        public async Task<string> RegisterUser(UserDto userToRegister)
        {
            // Registrera användare i API:t

            var response = await _http.PostAsJsonAsync<UserDto>("api/users", userToRegister);

            var token = await response.Content.ReadAsStringAsync();

            return token;
        }

        public async Task<string> LogInUser(UserDto userToLogin)
        {
            //Logga in användare
            var response = await _http.PostAsJsonAsync<UserDto>("api/users/login", userToLogin);

            string token = await response.Content.ReadAsStringAsync();

            return token;
        }

        public async Task<LoginDto> CheckUserLogin(string token)
        {
            // Use token to check user status (logged in, admin, banned, deleted...)

            var response = await _http.GetFromJsonAsync<LoginDto>($"api/users/check?accessToken={token}");

            return response;
        }

        public async Task<string> CreateInterest(InterestModel interestToAdd)
        {
            //skapa interest
            var response = await _http.PostAsJsonAsync<InterestModel>("api/users/createInterest", interestToAdd);

            string interestId = await response.Content.ReadAsStringAsync();

            return interestId;
        }
    }
}
