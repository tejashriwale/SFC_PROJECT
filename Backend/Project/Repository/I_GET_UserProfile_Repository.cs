using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_GET_UserProfile_Repository
    {
        Task<List<_GET_UserProfile_>> getasync(getByIdInput input);

        Task<List<_GET_UserProfile_>> GetByGroupAsync(getByIdInput input);
    }
}
