using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CashDetailsRepository
    {
        Task<List<GET_CashDetails>> getasync(GET_CashDetailsDomain input);
    }
}
