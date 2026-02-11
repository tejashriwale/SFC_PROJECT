using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_GoodReceiptNoteDataByIdRepository
    {
        Task<List<GET_GoodReceiptNoteDataById>> getasync(GET_GoodReceiptNoteDataByIdDomain input);
    }
}
