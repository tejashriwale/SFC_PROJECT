using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_POrderDetailsDataRepository
    {
        Task<List<GET_POrderDetailsData>>getasync(GET_POrderDetailsDataDomain input);
    }
}
