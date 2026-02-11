using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_FarmerCropBillDataRepository
    {
        Task<List<GET_FarmerCropBillData>> getasync(GET_FarmerCropBillDataDomain input);
    }
}
