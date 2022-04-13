using Fora.Shared;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace Fora.Client.Services
{
    public class UserManager : IUserManager
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigationManager;

        public UserManager(HttpClient http, ILocalStorageService localStorage, NavigationManager navigationManager )
        {
            _http = http;
            _localStorage = localStorage;
            _navigationManager = navigationManager;
        }

        public async Task DeleteUser(string token)
        {
            var deleteResponse = await _http.DeleteAsync($"api/users/delete/{token}");
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
            token = token.Replace("\"", "");

            // Use token to check user status (logged in, admin, banned, deleted...)

           
            var loginResponse = await _http.GetAsync($"api/users/check/{token}");


            if (loginResponse.IsSuccessStatusCode)
            {
                var result = await loginResponse.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<LoginDto>(result);

                return data;
            }

            return null;
        }

        public async Task LogOutUser()
        {
            
            await _localStorage.RemoveItemAsync("token");
            _navigationManager.NavigateTo("/");

        }

        public async Task ChangePasswordUser(UserDto user, string newPassword, string token)
        {

            var changePasswordResponse = await _http.PostAsJsonAsync($"api/users/change?newPassword={newPassword}&token={token}", user);
        }

    }
}
