using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_GateEntryDataByVehnoRepository
    {
        Task<List<GET_GateEntryDataByVehno>> getasync(GET_GateEntryDataByVehnoDomain input);
    }
}
