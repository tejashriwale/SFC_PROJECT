using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariApprovalListRepository
    {
        Task<List<GET_VyapariApprovalList>> getasync(GET_VyapariApprovalListDomain input);
    }
}
