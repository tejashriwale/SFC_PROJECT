using Project.Model.Domain;

namespace Project.Repository
{
    public interface IAddUpdPReturnMasterRepository
    {
        Task<List<ApiResponseMessage>>getasync(AddUpdPReturnMaster pReturnMaster);
    }
}
