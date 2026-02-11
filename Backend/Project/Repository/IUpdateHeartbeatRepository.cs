using Project.Model.Domain;

namespace Project.Repository
{
    public interface IUpdateHeartbeatRepository
    {
        Task<List<ApiResponseMessage>> getasync(UpdateHeartbeat input);
    }
}
