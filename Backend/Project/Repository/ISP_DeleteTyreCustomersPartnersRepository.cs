using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteTyreCustomersPartnersRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteTyreCustomersPartners input);
    }
}
