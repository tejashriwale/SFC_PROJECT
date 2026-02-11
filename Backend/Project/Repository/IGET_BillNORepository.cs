using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_BillNORepository
    {
        Task<List<GET_BillNO>> getasync(GET_BillNODomain input);
    }
}
