using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SChallanDetailsByIdRepository
    {
        Task<List<GET_SChallanDetailsById>>getasync(GET_SChallanDetailsByIdDomain input);
    }
}
