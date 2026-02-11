using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SQuotationDetailByIdRepository
    {
        Task<List<GET_SQuotationDetailById>>getasync(GET_SQuotationDetailByIdDomain input);
    }
}
