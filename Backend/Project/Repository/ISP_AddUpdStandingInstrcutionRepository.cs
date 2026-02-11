using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdStandingInstrcutionRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdStandingInstrcution> input);
    }
}
