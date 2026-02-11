using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_GateEntryDetailAllAppRepository
    {
        Task<List<GET_GateEntryDetailAllApp>>getasync(GET_GateEntryDetailAllAppDomain input);
    }
}
