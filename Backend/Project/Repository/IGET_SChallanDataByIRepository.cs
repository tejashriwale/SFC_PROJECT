using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SChallanDataByIRepository
    {
        Task<List<GET_SChallanDataByID>>getasync(GET_SChallanDataByIDDomain input);
    }
}
