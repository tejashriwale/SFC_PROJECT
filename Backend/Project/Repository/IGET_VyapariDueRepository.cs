using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariDueRepository
    {
        Task<List<GET_VyapariDue>> getasync(GET_VyapariDueDomain input);
    }
}
