using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_RegulerVaypariNameRepository
    {
        Task<List<GET_RegulerVaypariName>> getasync(GET_RegulerVaypariNameDomain input);
    }
}
