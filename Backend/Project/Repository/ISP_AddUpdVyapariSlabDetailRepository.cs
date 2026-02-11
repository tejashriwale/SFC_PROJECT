using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdVyapariSlabDetailRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdVyapariSlabDetail> input);
    }
}

