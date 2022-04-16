﻿using Fora.Shared;
namespace Fora.Client.Services
{
    public interface IThreadManager
    {
        Task<List<ThreadModel>> GetThreads();
        Task<string> CreateNewThread(ThreadModel threadToCreate, string token);
        Task<List<MessageModel>> GetThreadMessages();
        Task<string> CreateNewMessage(MessageModel messageToCreate, string token);

    }
}