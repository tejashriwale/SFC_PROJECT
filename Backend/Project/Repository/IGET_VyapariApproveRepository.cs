using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariApproveRepository
    {
        Task<List<GET_VyapariApprove>> getasync(GET_VyapariApproveDomain input);

        Task<List<GET_VyapariApprove>> getkeyword(GET_VyapariApproveDomain input);
    }
}
