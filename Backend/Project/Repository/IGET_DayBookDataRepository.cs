using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_DayBookDataRepository
    {
        Task<List<GET_DayBookData>> getasync(GET_DayBookDataDomain input);
    }
}
