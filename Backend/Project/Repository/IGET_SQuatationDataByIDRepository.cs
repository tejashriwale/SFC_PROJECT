using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SQuatationDataByIDRepository
    {
        Task<List<GET_SQuatationDataByID>> getasync(GET_SQuatationDataByIDDomain input);
    }
}
