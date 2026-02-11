using Project.Model.Domain;
using Project.Model.Model;

namespace Project.Repository
{
    public interface IServiceMasterRepository
    {
        Task<List<ApiResponseMessage>> getasync(ServiceMaster serviceMaster );
    }
}
