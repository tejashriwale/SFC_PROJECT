using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_SP_DeleteCustomersPartners_Repository
    {
        Task<List<ApiResponseMessage>>getasync(_SP_DeleteCustomersPartners_ input);
    }
}
