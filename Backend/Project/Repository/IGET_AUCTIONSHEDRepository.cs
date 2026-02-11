using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGET_AUCTIONSHEDRepository
    {
        Task<List<GET_AUCTIONSHED>>getasync(GET_AUCTIONSHEDDomain input);
        Task<List<GET_AUCTIONSHED>>getkeyword(GET_AUCTIONSHEDDomain input);
    }
}
