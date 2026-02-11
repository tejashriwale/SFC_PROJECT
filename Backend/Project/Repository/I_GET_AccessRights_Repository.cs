using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_GET_AccessRights_Repository
    {
        Task<List<_GET_AccessRights_>> AccessRightAllAsync();

        Task<List<_GET_AccessRights_>> GetByGroupAsync(_GET_AccessRights_Domain input);
    }
}
