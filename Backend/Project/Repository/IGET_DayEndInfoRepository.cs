using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGET_DayEndInfoRepository
    {
        Task<List<GET_DayEndInfo>> getasync(GET_DayEndInfoDomain input);
    }
}
