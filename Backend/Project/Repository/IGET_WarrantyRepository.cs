using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_WarrantyRepository
    {
        Task<List<GET_Warranty>>getasync(GET_WarrantyDomain input);

        Task<List<GET_Warranty>>get(GET_WarrantyDomain input);
    }
}
