using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_GoodReceiptNoteMasterRepository
    {
        Task<List<GET_GoodReceiptNoteMaster>> getasync(GET_GoodReceiptNoteMasterDomain input);

        Task<List<GET_GoodReceiptNoteMaster>> getkeyword(GET_GoodReceiptNoteMasterDomain input);
    }
}
