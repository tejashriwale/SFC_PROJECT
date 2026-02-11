using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGET_FarmerBillChargesVyapariRepository
    {
        Task<List<GET_FarmerBillChargesVyapari>> getasync(GET_FarmerBillChargesVyapariDomain input);
    }
}
