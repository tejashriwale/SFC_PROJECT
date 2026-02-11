using Project.Model;
using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_AuctionUserRepository
    {
        Task<List<GET_AuctionUser>> GetAll(GET_AuctionUserDomain input);
    }
}
