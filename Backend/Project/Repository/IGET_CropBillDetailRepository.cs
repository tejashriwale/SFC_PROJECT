using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CropBillDetailRepository
    {
        Task<List<GET_CropBillDetail>> getasync(GET_CropBillDetailDomain input);
    }
}
