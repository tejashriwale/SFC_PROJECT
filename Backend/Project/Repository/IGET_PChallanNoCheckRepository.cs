using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PChallanNoCheckRepository
    {
        Task<List<GET_PChallanNoCheck>> getasync(GET_PChallanNoCheckDomain input);
    }
}
