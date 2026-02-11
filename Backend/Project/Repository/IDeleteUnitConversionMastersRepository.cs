using Project.Model.Domain;

namespace Project.Repository
{
    public interface IDeleteUnitConversionMastersRepository
    {
        Task<List<ApiResponseMessage>>getasync(DeleteUnitConversionMasters input);
    }
}
