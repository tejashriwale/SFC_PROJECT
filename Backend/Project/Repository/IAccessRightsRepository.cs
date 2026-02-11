using Project.Model.Domain;

namespace Project.Repository
{
    public interface IAccessRightsRepository
    {
        Task<List<ApiResponseMessage>> getasync(AccessRights accessRights);
    }
}
