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

        public async Task AddUserInterest(InterestModel interestToAdd, string token)
        {
            var response = await _httpClient.PostAsJsonAsync<InterestModel>($"api/Interests/addinterest?token={token}", interestToAdd);
        }

    }
}
