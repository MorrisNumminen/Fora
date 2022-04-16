using Fora.Shared;
using System.Net.Http.Json;
namespace Fora.Client.Services
{
    public class ThreadManager : IThreadManager
    {
        private readonly HttpClient _httpClient;

        public ThreadManager(HttpClient http)
        {
            _httpClient = http;
        }

        public async Task<List<ThreadModel>> GetThreads()
        {
            return await _httpClient.GetFromJsonAsync<List<ThreadModel>>("api/Threads/getthreads");             
        }

        public async Task<string> CreateNewThread(ThreadModel threadToCreate, string token)
        {
            // Lägg till ett interest i db

            var response = await _httpClient.PostAsJsonAsync<ThreadModel>($"api/Threads/createthread?token={token}", threadToCreate);

            return null;

        }

        public async Task<List<MessageModel>> GetThreadMessages()
        {
            return await _httpClient.GetFromJsonAsync<List<MessageModel>>("api/Threads/getthreadmessages");
        }

        public async Task<string> CreateNewMessage(MessageModel messageToCreate, string token)
        {
            // Lägg till ett message i db

            var response = await _httpClient.PostAsJsonAsync<MessageModel>($"api/Threads/createmessage?token={token}", messageToCreate);

            return null;

        }



    }
}
