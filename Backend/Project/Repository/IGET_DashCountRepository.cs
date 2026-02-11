using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGET_DashCountRepository
    {
        Task<List<GET_DashCount>>getasync( );
    }
}
