using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGet_AdminVideoAppRepository
    {
        Task<List<Get_AdminVideoApp>> GetAll();
    }
}
