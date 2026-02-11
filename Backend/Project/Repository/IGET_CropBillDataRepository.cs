using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CropBillDataRepository
    {
        Task<List<GET_CropBillData>> getasync(GET_CropBillDataDomain input);
    }
}
