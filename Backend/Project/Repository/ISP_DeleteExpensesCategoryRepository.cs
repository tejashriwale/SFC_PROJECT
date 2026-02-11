using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteExpensesCategoryRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteExpensesCategory input);
    }
}
