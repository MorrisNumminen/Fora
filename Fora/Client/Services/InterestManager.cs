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

        public async Task<string> CreateInterest(InterestModel interestToCreate)
        {
            // Lägg till 

            var response = await _httpClient.PostAsJsonAsync<InterestModel>("api/createinterest", interestToCreate);

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

    }
}
