using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_FarmerBillDataRepository
    {
        Task<List<GET_FarmerBillData>> getasync(GET_FarmerBillDataDomain input);
    }
}
