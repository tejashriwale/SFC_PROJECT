using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_REPORT_SRepository
    {
        Task<List<GET_REPORT_S>> getasync(GET_REPORT_SDomain input);
    }
}
