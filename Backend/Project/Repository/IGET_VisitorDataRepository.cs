using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VisitorDataRepository
    {
        Task<List<GET_VisitorData>> getasync(GET_VisitorDataDomain input);
    }
}
