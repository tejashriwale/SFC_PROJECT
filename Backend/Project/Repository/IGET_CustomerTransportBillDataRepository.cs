using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CustomerTransportBillDataRepository
    {
        Task<List<GET_CustomerTransportBillData>> getasync(GET_CustomerTransportBillDataDomain input);
    }
}
