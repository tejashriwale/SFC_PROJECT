using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_FBillPrintRepository
    {
        Task<List<GET_FBillPrint>> getasync(GET_FBillPrintDomain input);
    }
}
