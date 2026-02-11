using Project.Model.Domain;

namespace Project.Repository
{
    public interface IAddUpdPChallanMasterRepository
    {
        Task<List<ApiResponseMessage>>getasync(AddUpdPChallanMaster addUpdPChallanMaster);
    }
}
