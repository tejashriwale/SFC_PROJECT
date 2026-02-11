using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PRequisitionNosearchRepository
    {
        Task<List<GET_PRequisitionNosearch>>getasync(GET_PRequisitionNosearchDomain  gET_PRequisition);
    }
}
