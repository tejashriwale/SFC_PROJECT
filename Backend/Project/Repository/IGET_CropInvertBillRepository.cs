using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CropInvertBillRepository
    {
        Task<List<GET_CropInvertBill>> getasync(GET_CropInvertBillDomain input);
    }
}
