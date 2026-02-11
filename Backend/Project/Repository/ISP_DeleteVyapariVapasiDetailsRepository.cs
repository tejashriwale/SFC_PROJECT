using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteVyapariVapasiDetailsRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteVyapariVapasiDetails input);
    }
}
