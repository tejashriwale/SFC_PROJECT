using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SBillDetailsRepository
    {
        Task<List<GET_SBillDetails>> getasync(GET_SBillDetailsDomain input);
    }
}
