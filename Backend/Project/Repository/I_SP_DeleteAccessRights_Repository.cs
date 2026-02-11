using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_SP_DeleteAccessRights_Repository
    {
        Task<List<ApiResponseMessage>> getasync(_SP_DeleteAccessRights_ sP_DeleteAccessRights_);
    }
}
