using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VisitorMasterApprovalRepository
    {
        Task<List<GET_VisitorMasterApproval>> getasync(GET_VisitorMasterApprovalDomain input);
        Task<List<GET_VisitorMasterApproval>> getkeyword(GET_VisitorMasterApprovalDomain input);
    }
}
