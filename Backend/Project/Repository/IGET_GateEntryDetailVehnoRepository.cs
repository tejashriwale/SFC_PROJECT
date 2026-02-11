using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_GateEntryDetailVehnoRepository
    {
        Task<List<GET_GateEntryDetailVehno>> getasync(GET_GateEntryDetailVehnoDomain input);
    }
}
