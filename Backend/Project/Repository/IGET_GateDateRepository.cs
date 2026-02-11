using Project.Model;

namespace Project.Repository
{
    public interface IGET_GateDateRepository
    {
        Task<List<GET_GateDate>> getall();
    }
}
