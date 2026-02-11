using Project.Model.Domain;

namespace Project.Repository
{
    public interface IDeleteUserMastersRepository
    {
        Task<List<ApiResponseMessage>>getasync(DeleteUserMasters input);
    }
}
