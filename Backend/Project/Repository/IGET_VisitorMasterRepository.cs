using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VisitorMasterRepository
    {
        Task<List<GET_VisitorMaster>> getasync(GET_VisitorMasterDomain input);
        Task<List<GET_VisitorMaster>> getkeyword(GET_VisitorMasterDomain input);
    }
}
