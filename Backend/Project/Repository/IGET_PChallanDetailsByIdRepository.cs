using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PChallanDetailsByIdRepository
    {
        Task<List<GET_PChallanDetailsById>> getasync(GET_PChallanDetailsByIdDomain input);
    }
}
