using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariApprovalByIDRepository
    {
        Task<List<GET_VyapariApprovalByID>>getasync(GET_VyapariApprovalByIDDomain input);
    }
}
