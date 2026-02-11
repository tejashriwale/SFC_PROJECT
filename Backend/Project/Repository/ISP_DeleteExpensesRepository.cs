using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteExpensesRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_DeleteExpensesDomain input);
    }
}
