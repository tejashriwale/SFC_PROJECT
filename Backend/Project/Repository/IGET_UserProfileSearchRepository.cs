using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_UserProfileSearchRepository
    {
        Task<List<GET_UserProfileSearchDomain>> GetAll(GET_UserProfileSearch input);
    }
}
