using Project.Model.Domain;
using Project.Model.Model;

namespace Project.Repository
{
    public interface ISP_AddUpdAuctionShedDeatilRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdAuctionShedDeatil> input);
    }
}
