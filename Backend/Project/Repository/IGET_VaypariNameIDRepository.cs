using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VaypariNameIDRepository
    {
        Task<List<GET_VaypariNameID>>getasync(GET_VaypariNameIDDomain input);
    }
}
