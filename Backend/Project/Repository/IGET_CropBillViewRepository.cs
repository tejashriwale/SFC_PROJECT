using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CropBillViewRepository
    {
        Task<List<GET_CropBillView>> getasync(GET_CropBillViewDomain input);
    }
}
