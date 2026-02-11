using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_UpdTranspoterProfileRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_UpdTranspoterProfile input);
    }
}
