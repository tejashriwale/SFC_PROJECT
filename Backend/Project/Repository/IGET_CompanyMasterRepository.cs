using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CompanyMasterRepository
    {
        Task<List<GET_CompanyMaster>>getasync(GET_CompanyMasterDomain input);
    }
}
