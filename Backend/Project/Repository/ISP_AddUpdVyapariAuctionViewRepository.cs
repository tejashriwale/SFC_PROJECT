using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdVyapariAuctionViewRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdVyapariAuctionView input);
    }
}
