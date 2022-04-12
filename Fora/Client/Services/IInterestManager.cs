using Fora.Shared;

namespace Fora.Client.Services
{
    public interface IInterestManager
    {
        Task<List<InterestModel>> GetInterests();

        Task<string> CreateInterest(InterestModel interestToCreate, string token);

    }
}