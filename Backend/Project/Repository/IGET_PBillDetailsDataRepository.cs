using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PBillDetailsDataRepository
    {
        Task<List<GET_PBillDetailsData>>getasync(GET_PBillDetailsDataDomain input);
    }
}
