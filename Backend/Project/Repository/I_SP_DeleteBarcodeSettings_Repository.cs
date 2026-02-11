using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_SP_DeleteBarcodeSettings_Repository
    {
        Task<List<ApiResponseMessage>> getasync(_SP_DeleteBarcodeSettings_ input);
    }
}
