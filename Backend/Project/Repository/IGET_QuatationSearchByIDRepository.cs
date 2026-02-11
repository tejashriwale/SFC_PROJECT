using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_QuatationSearchByIDRepository
    {
        Task<List<GET_QuatationSearchByID>>getasync(GET_QuatationSearchByIDDomain input);
    }
}
