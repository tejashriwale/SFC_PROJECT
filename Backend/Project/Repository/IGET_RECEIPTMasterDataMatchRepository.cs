using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_RECEIPTMasterDataMatchRepository
    {
        Task<List<GET_RECEIPTMasterDataMatch>> getasync(GET_RECEIPTMasterDataMatchDomain input);
        Task<List<GET_RECEIPTMasterDataMatch>> getkeyword(GET_RECEIPTMasterDataMatchDomain input);
    }
}
