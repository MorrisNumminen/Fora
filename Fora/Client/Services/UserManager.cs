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

       public async Task<ApplicationUser> GetAsync(string token)
        {
            var response = await _http.GetAsync($"api/users/{token}");
            var result = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<ApplicationUser>(result);

            return user;
        }

        public async Task<UserModel> GetDbAsync(string token)
        {
            var response = await _http.GetAsync($"api/users/DbAsync/{token}");
            var result = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserModel>(result);

            return user;
        }

        public async Task<string> RegisterUser(UserDto userToRegister)
        {
            // Registrera användare i API:t

            var response = await _http.PostAsJsonAsync<UserDto>("api/users", userToRegister);

            if (response.IsSuccessStatusCode)
            {
                _navigationManager.NavigateTo("Login");
            }

            
            return await response.Content.ReadAsStringAsync();
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

           
            var loginResponse = await _http.GetAsync($"api/users/check/{token}");


            if (loginResponse.IsSuccessStatusCode)
            {
                var result = await loginResponse.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<LoginDto>(result);

                return data;
            }

            return null;
        }

        public async Task LogOutUser(string token)
        {  
            await _localStorage.RemoveItemAsync("token");
        }

        public async Task DeleteUser(string token)
        {
            var deleteResponse = await _http.DeleteAsync($"api/users/delete/{token}");
        }

        public async Task BanUser(string token)
        {
            var banResponse = await _http.GetAsync($"api/users/ban/{token}");
        }

        public async Task UnbanUser(string token)
        {
            var unbanResponse = await _http.GetAsync($"api/users/unban/{token}");
        }

        public async Task ChangePasswordUser(UserDto user, string newPassword, string token)
        {

            var changePasswordResponse = await _http.PutAsJsonAsync($"api/users/change?newPassword={newPassword}&token={token}", user);
        }

    }
}
