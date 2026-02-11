using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_BILLCOUNTSRepository
    {
        Task<List<GET_BILLCOUNTS>>getasync(GET_BILLCOUNTSDomain input);
    }
}
