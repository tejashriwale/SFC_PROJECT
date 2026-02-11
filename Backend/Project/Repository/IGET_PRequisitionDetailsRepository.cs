using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PRequisitionDetailsRepository
    {
        Task<List<GET_PRequisitionDetails>>getasync(RepuisitonDomain input);
    }
}
