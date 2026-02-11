using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_SP_DeleteCounterMaster_Repository
    {
        Task<List<ApiResponseMessage>> getasync(_SP_DeleteCounterMaster_ input);
    }
}
