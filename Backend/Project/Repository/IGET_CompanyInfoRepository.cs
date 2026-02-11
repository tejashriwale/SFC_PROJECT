using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CompanyInfoRepository
    {
        Task<List<GET_CompanyInfo>> getasync(GET_CompanyInfoDomain input);
        Task<List<GET_CompanyInfo>> getkeyword(GET_CompanyInfoDomain input);
    }
}
