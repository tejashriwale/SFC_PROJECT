using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_BOMPackingRepository
    {
        Task<List<GET_BOMPacking>> getasync(GET_BOMPackingDomain input);

        Task<List<GET_BOMPacking>> GetByGroupAsync(GET_BOMPackingDomain input);

        Task<List<GET_BOMPacking>> getkeyword(GET_BOMPackingDomain input);
    }
}
