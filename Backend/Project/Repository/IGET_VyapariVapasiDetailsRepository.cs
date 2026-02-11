using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGET_VyapariVapasiDetailsRepository
    {
        Task<List<GET_VyapariVapasiDetails>> getasync(GET_VyapariVapasiDetailsDomain input);
    }
}
