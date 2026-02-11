using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_GET_UnitConversionMasters_Repository
    {
        Task<List<_GET_UnitConversionMasters_>> getasync(getByIdInput input);

        Task<List<_GET_UnitConversionMasters_>> GetByGroupAsync(getByIdInput input);

        Task<List<_GET_UnitConversionMasters_>> searchasync(getByIdInput input);
    }
}
