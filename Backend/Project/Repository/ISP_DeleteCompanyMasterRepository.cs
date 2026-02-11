using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteCompanyMasterRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_DeleteCompanyMaster input);
    }
}
