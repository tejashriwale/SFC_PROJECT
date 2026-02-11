using Project.Model.Domain;

namespace Project.Model.DTO
{
    public interface IGET_CustomerDetailsByIDRepository
    {
        Task<List<GET_CustomerDetailsByID>>getasync(CounterMasterDomain masterDomain);
    }
}
