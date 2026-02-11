using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGET_SRequisitionDetailByIdRepository
    {
       Task<List<GET_SRequisitionDetailById>>getaqsync(GET_SRequisitionDetailByIdDomain input);
    }
}
