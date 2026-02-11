using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CashDetailsNearbyCashRepository
    {
        Task<List<GET_CashDetailsNearbyCash>> getasync(GET_CashDetailsNearbyCashDomain input);
    }
}
