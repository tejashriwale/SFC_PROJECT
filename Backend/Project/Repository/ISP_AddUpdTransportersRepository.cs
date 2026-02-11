using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdTransportersRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdTransporters input);
    }
}
