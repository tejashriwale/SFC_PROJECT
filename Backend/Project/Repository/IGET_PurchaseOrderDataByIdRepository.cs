using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PurchaseOrderDataByIdRepository
    {
        Task<List<GET_PurchaseOrderDataById>>getasync(GET_PurchaseOrderDataByIdDomain input);
    }
}
