using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PReturnDataByIDRepository
    {
        Task<List<GET_PReturnDataByID>>getasync(GET_PReturnDataByIDDomain input);
    }
}
