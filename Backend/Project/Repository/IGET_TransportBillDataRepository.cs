using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_TransportBillDataRepository
    {
        Task<List<GET_TransportBillData>> getasync(GET_TransportBillDataDomain input);
    }
}
