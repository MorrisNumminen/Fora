using Fora.Shared;

namespace Fora.Client.Services
{
    public interface IInterestManager
    {
        Task<List<InterestModel>> GetInterests();

        Task AddUserInterests(InterestModel interest, string token);
        Task RemoveUserInterests(InterestModel removeInterest, string token);
        Task<string> CreateInterest(InterestModel interestToCreate, string token);
        Task<List<InterestModel>> GetUserInterests(string token);
    }
}

