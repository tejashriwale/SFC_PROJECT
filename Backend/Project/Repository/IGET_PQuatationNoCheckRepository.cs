using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGET_PQuatationNoCheckRepository
    {
        Task<List<GET_PQuatationNoCheck>> getasync(GET_PQuatationNoCheckDomain input);
    }
}
