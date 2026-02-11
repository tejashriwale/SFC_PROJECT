using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PRequisitionNumberSearchRepository
    {
        Task<List<GET_PRequisitionNumberSearch>> getasync(GET_PRequisitionNosearchDomain gET_PRequisition);
    }
}
