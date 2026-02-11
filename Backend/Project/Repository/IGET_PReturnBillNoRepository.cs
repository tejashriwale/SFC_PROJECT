using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PReturnBillNoRepository
    {
        Task<List<GET_PReturnBillNo>> getasync(GET_PReturnBillNoDomain input);
    }
}
