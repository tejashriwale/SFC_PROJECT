using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_FarmerBillChargesdRepository
    {
        Task<List<GET_FarmerBillCharges>> getasync(GET_FarmerBillChargesDomain input);
    }
}
