using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CustomerTranDataRepository
    {
        Task<List<GET_CustomerTranData>> getasync(GET_CustomerTranDataDomain input);
    }
}
