using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SReturnBillNoRepository
    {
        Task<List<GET_SReturnBillNo>> getasync(GET_SReturnBillNoDomai input);
    }
}
