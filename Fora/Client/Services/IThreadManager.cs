using Fora.Shared;
namespace Fora.Client.Services
{
    public interface IThreadManager
    {
        Task<List<ThreadModel>> GetThreads();
        Task<string> CreateNewThread(ThreadModel threadToCreate, string token);
        Task<List<MessageModel?>> GetThreadMessages(int threadId);
        Task<string> CreateNewMessage(MessageModel messageToCreate, string token);
        Task PutMessageAsync(int messageId, string newMessage);
        Task MarkAsDeletedMessageAsync(MessageDto message);
    }
}