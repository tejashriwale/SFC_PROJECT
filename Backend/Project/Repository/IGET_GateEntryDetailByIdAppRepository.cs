using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_GateEntryDetailByIdAppRepository
    {
        Task<List<GET_GateEntryDetailByIdApp>> getasync(GET_GateEntryDetailByIdAppDomain input);
    }
}
