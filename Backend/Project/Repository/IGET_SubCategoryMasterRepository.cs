using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SubCategoryMasterRepository
    {
        Task<List<GET_SubCategoryMaster>>getasync(GET_SubCategoryMasterDomain input);
        Task<List<GET_SubCategoryMaster>>getkeyword(GET_SubCategoryMasterDomain input);
    }
}
