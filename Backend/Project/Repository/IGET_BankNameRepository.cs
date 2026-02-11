using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_BankNameRepository
    {
        Task<List<GET_BankName>>getasync(GET_BankNameDomain input);
    }
}
