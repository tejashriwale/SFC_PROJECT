using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteVisitorMasterRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteVisitorMaster input);
    }
}
