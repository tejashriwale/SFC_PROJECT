using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_MeassageQuatationRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_MeassageQuatation sP_MeassageQuatation);
    }
}
