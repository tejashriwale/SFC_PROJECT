using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_ShedulerDetailDataRepository
    {
        Task<List<GET_ShedulerDetailData>> getasync(GET_ShedulerDetailDataDomain input);
    }
}
