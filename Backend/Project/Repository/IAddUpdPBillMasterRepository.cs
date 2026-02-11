using Project.Model.Domain;

namespace Project.Repository
{
    public interface IAddUpdPBillMasterRepository
    {
        Task<List<ApiResponseMessage>>getasync(AddUpdPBillMaster pBillMaster);
    }
}
