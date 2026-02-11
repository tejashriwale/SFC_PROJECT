using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CropMonthDataRepository
    {
        Task<List<GET_CropMonthData>> getasync(GET_CropMonthDataDomain customer);
    }
}
