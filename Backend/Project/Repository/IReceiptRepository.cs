using Project.Model.Domain;

namespace Project.Repository
{
    public interface IReceiptRepository
    {
        Task<bool> AddOrUpdateReceiptAsync(ReceiptRequest request);
    }
}

