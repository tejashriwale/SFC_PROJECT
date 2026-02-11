using Project.Model.Domain;

namespace Project.Model.DTO
{
    public interface I_SP_DeleteRackMaster_Repository
    {
        Task<List<ApiResponseMessage>>getasync(_SP_DeleteRackMaster_ input);
    }
}
