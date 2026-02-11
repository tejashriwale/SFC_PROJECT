using Project.Model.Domain;

namespace Project.Repository
{
    public interface IBarcodeSettingsRepository
    {
        Task<List<ApiResponseMessage>> getasync(BarcodeSettings barcodeSettings);

    }
}
