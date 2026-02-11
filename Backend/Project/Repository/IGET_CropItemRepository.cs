using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CropItemRepository
    {
        Task<List<GET_CropItem>>getasync(GET_CropItemDomain input);
        Task<List<GET_CropItem>>getkeyword(GET_CropItemDomain input);
    }
}
