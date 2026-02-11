using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SRequisitionNoSearchRepository
    {
        Task<List<GET_SRequisitionNoSearch>>getasync(GET_SRequisitionNoSearchDomain input);
    }
}
