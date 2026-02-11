using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdVisitorMasterRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdVisitorMaster input);
    }
}
