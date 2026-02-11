using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariPurchaseBillRRepositorycs
    {
        Task<List<GET_VyapariPurchaseBillR>> getasync(GET_VyapariPurchaseBillRDomain input);
    }
}
