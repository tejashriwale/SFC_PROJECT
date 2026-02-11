using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteVehicaldetailsRepository 
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteVehicaldetails input);
    }
}
