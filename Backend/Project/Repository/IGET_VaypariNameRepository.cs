using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VaypariNameRepository
    {
        Task<List<GET_VaypariName>>getasync(GET_VaypariNameDomain input);
    }
}
