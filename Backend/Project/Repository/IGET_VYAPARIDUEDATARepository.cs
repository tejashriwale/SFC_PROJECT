using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGET_VYAPARIDUEDATARepository
    {
        Task<List<GET_VYAPARIDUEDATA>> getasync(GET_VYAPARIDUEDATADomain input);
    }
}
