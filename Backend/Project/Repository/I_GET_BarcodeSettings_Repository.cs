using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_GET_BarcodeSettings_Repository
    {
        Task<List<_GET_BarcodeSettings_>> getasync(MasterInput input);

        Task<List<_GET_BarcodeSettings_>> GetByGroupAsync(MasterInput input);
    }
}
