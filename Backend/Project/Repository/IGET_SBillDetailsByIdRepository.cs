using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SBillDetailsByIdRepository
    {
        Task<List<GET_SBillDetailsById>> getasync(GET_SBillDetailsByIdDomain input);
    }
}
