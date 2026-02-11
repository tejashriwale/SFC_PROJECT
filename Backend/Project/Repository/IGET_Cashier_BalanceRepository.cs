using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_Cashier_BalanceRepository
    {
        Task<List<GET_Cashier_Balance>> GetAll();
    }
}
