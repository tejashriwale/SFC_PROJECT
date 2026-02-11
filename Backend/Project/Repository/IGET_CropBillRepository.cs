using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CropBillRepository
    {
        Task<List<GET_CropBill>> getasync(GET_CropBillDomain input);
    }
}
