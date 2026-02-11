using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_GateEntry_VTRepository
    {
        Task<List<GET_GateEntry_VT>> getasync(GET_GateEntry_VTDomain input);
    }
}
