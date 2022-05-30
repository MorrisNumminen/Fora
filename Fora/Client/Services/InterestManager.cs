using Fora.Shared;
using System.Net.Http.Json;
namespace Fora.Client.Services
{
    public class InterestManager : IInterestManager
    {
        private readonly HttpClient _httpClient;

        public InterestManager(HttpClient http)
        {
            _httpClient = http;
        }

        public async Task<List<InterestModel>> GetInterests()
        {
            return await _httpClient.GetFromJsonAsync<List<InterestModel>>("api/Interests/getinterests");
        }

        public async Task<string> CreateInterest(InterestModel interestToCreate, string token)
        {
            // Lägg till ett interest i db

            var response = await _httpClient.PostAsJsonAsync<InterestModel>($"api/Interests/createinterest?token={token}", interestToCreate);

            return null;
        }

        public async Task DeleteInterest(int interestId)
        {
            // Ta bort ett interest i db
            var response = await _httpClient.DeleteAsync($"api/Interests/deleteinterest/{interestId}");
        }
        

        public async Task AddUserInterests(int interestId, string token)
        {
             var response = await _httpClient.PostAsJsonAsync<int>($"api/Interests/AddUserInterest?token={token}", interestId);
        }

        public async Task RemoveUserInterests(InterestModel removeInterest, string token)
        {
             await _httpClient.PostAsJsonAsync<InterestModel>($"api/Interests/RemoveUserInterest?token={token}", removeInterest);

        }

        public async Task<List<InterestModel>> GetUserInterests(string token)
        {
            var response = await _httpClient.GetFromJsonAsync<List<InterestModel>>($"api/Interests/UserInterests?token={token}");

            return response;
        }
    }
}
