using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_UpdateCustomerDetailsRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_UpdateCustomerDetails input);
    }
}
