using Project.Model;
using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_QuatationSearchNoRepository
    {
       Task<List<GET_QuatationSearchNo>>getasync (GET_QuatationSearchNoDomain input);
    }
}
