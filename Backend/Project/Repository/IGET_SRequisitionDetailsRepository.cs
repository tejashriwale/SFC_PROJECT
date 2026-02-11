using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SRequisitionDetailsRepository
    {
        Task<List<GET_SRequisitionDetails>>getasync(GET_SRequisitionDetailsDomain input);
    }
}
