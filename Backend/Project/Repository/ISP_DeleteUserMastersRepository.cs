using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteUserMastersRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteUserMasters input);
    }
}
