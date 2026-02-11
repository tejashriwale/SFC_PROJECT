using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_AccountACCTMRepository
    {
        Task<List<GET_AccountACCTM>> GetAll();
    }
}
