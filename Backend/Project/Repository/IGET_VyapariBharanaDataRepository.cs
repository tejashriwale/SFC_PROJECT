using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariBharanaDataRepository
    {
        Task<List<GET_VyapariBharanaData>> getasync(GET_VyapariBharanaDataDomain input);
    }
}
