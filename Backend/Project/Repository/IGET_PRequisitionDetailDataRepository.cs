using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PRequisitionDetailDataRepository
    {
        Task<List<GET_PRequisitionDetailData>>getasync(GET_PRequisitionDetailDataDomain input);
    }
}
