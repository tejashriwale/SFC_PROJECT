using Project.Model.Domain;

namespace Project.Repository
{
    public interface IRackMasterRepository
    {
        Task<List<ApiResponseMessage>> getasync(RackMaster rackMaster);
    }
}
