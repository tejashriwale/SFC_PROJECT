using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteSchedulerDetailRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteSchedulerDetail input);
    }
}

