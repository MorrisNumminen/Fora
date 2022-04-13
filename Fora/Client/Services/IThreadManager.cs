using Fora.Shared;
namespace Fora.Client.Services
{
    public interface IThreadManager
    {
        Task<List<ThreadModel>> GetThreads();

        Task<string> CreateNewThread(ThreadModel threadToCreate, string token);


    }
}