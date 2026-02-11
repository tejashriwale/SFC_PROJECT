using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGET_GateEntryDetailByIdRepository
    {
        Task<List<GET_GateEntryDetailById>> getasync(GET_GateEntryDetailByIdDomain inupt);
       
    }
}
