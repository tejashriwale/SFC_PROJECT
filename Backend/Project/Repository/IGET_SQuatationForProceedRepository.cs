using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGET_SQuatationForProceedRepository
    {
        Task<List<GET_SQuatationForProceed>> getasync(GET_SQuatationForProceedDomain input);
    }
}
