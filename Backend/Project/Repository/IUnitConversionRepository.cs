using Project.Model.Domain;

namespace Project.Repository
{
    public interface IUnitConversionRepository
    {
        Task<List<ApiResponseMessage>> getasync(UnitConversion unitConversion);
    }
}
