using Project.Model.Domain;

namespace Project.Repository
{
    public interface IAddUpdPRequisitionMasterRepository
    {
        Task<List<ApiResponseMessage>>getasync(AddUpdPRequisitionMaster pRequisitionMaster);
    }
}
