using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SRequisitionDataByIDRepository
    {
        Task<List<GET_SRequisitionDataByID>>getsync(GET_SRequisitionDataByIDDomain input);
    }
}
