using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SOrderDetailsRepository
    {
        Task<List<GET_SOrderDetails>> getasync(GET_SOrderDetailsDomain input);
    }
}
