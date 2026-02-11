using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IUserMastersRepository
    {
        Task<List<ApiResponseMessage>> getasync (UserMasters userMasters);
    }
}
