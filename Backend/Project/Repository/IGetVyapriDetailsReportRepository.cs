using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGetVyapriDetailsReportRepository
    {
        Task<List<GetVyapriDetailsReport>> getasync(GetVyapriDetailsReportDomain input);
    }
}
