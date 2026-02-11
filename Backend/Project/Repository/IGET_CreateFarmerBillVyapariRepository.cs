using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CreateFarmerBillVyapariRepository
    {
        Task<List<GET_CreateFarmerBillVyapari>> getasync(GET_CreateFarmerBillVyapariDomain input);
    }
}
