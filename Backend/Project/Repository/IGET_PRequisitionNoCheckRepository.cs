using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PRequisitionNoCheckRepository
    {
        Task<List<GET_PRequisitionNoCheck>> getasync(GET_PRequisitionNoCheckDomain input);
    }
}
