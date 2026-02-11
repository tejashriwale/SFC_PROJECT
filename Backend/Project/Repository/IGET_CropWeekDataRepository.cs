using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CropWeekDataRepository
    {
        Task<List<GET_CropWeekData>> getasync(GET_CropWeekDataDomain input);
    }
}
