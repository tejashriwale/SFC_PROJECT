using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdWarrrantyRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_AddUpdWarrranty input);
    }
}
