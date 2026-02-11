using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_FarmerPaymentSummaryRepository
    {
        Task<List<GET_FarmerPaymentSummary>> getasync(GET_FarmerPaymentSummaryDomain input);
        Task<bool> UpdatePaymentDateAsync(UpdatePaymentDateDomain input);
    }
}
