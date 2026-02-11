using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CashCounterRepository
    {
        Task<List<GET_CashCounter>>getasync(GET_CashCounterDomain input);
        Task<List<GET_CashCounter>>keyword(GET_CashCounterDomain input);
    }
}
