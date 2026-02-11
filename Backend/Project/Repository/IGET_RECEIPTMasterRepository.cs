using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_RECEIPTMasterRepository
    {
        Task<List<GET_RECEIPTMaster>>getasync(GET_RECEIPTMasterDomain input);
        Task<List<GET_RECEIPTMaster>>getkeyword(GET_RECEIPTMasterDomain input);
    }
}
