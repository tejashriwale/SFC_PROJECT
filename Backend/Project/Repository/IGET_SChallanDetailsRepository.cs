using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SChallanDetailsRepository
    {
        Task<List<GET_SChallanDetails>> getasync(GET_SChallanDetailsDomain input);
    }
}
