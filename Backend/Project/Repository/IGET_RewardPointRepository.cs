using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_RewardPointRepository
    {
        Task<List<GET_RewardPoint>> getasync(GET_RewardPointDomain input);
        Task<List<GET_RewardPoint>> getkeyword(GET_RewardPointDomain input);
    }
}
