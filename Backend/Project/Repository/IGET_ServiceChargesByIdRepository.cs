using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_ServiceChargesByIdRepository
    {
        Task<List<GET_ServiceChargesById>>getasync(GET_ServiceChargesByIdDomain input);
    }
}
