using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_QuatationDetailDataRepository
    {
        Task<List<GET_QuatationDetailData>>getasync(GET_QuatationDetailDataDomain input);
    }
}
