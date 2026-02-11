using Project.Model.Domain;
using Project.Model.Model;

namespace Project.Repository
{
    public interface IHSNMasterRepository
    {
        Task<List<ApiResponseMessage>> HSNMasterAsync(HSNMaster hSNMaster);
    }
}
