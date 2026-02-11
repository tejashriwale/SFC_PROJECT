using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CategoryMasterCaseRepository
    {
        Task<List<GET_CategoryMasterCase>> getasync(GET_CategoryMasterCaseDomain input);
    }
}
