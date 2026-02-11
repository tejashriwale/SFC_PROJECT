using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGET_SBillNoRepository
    {
        Task<List<GET_SBillNo>> getasync(GET_SBillNoDomain input);
    }
}
