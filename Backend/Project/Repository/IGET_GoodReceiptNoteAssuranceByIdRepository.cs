using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_GoodReceiptNoteAssuranceByIdRepository
    {
        Task<List<GET_GoodReceiptNoteAssuranceById>>getasync(GET_GoodReceiptNoteAssuranceByIdDomain input);
    }
}
