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

        public async Task<List<MessageModel?>> GetThreadMessages(int threadId)
        {

            HttpResponseMessage response = await _httpClient.GetAsync($"api/Threads/getthreadmessages/{threadId}");
            var test = response.Content.ReadAsStream();
            List<MessageModel?> message = null;                                     
            if (response.IsSuccessStatusCode)
            {
                message = await response.Content.ReadAsAsync<List<MessageModel?>>();
            }
            return message;
          
        }

        public async Task<string> CreateNewMessage(MessageModel messageToCreate, string token)
        {
            // Lägg till ett message i db

            var response = await _httpClient.PostAsJsonAsync<MessageModel>($"api/Threads/createmessage?token={token}", messageToCreate);

            return null;

        }
        
        public async Task DeleteMessage(int messageDelId, string token)
        {
            // Lägg till ett message i db
            Console.WriteLine("DeleteMessage() : 2");
            await _httpClient.PostAsJsonAsync<int>($"api/Threads/deletemessage?token={token}", messageDelId);
        }

        public async Task PutMessageAsync(MessageModel message)
        {
            
           var response = await _httpClient.PutAsJsonAsync<MessageModel>($"api/Threads/updatemessage", message);
        }
    }
}
