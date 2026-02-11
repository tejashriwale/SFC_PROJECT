using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_SP_DeleteServiceMaster_Repository
    {
        Task<List<ApiResponseMessage>> getasync(_SP_DeleteServiceMaster_ sP_DeleteServiceMaster_);
    }
}
