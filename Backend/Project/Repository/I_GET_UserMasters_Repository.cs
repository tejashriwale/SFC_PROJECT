using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_GET_UserMasters_Repository
    {
        Task<List<_GET_UserMasters_>> getasync(UserMasterDomain input);

        Task<List<_GET_UserMasters_>> GetByGroupAsync(UserMasterDomain input);
        Task<List<_GET_UserMasters_>> getkeyword(UserMasterDomain input);
    }
}
