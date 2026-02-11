using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdServiceChargeRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_AddUpdServiceCharge input);
    }
}
