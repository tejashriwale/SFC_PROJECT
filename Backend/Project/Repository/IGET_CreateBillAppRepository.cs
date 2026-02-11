using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CreateBillAppRepository
    {
        Task<List<GET_CreateBillApp>> getasync(GET_CreateBillAppDomain input);
    }
}
