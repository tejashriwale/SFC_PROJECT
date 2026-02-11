using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdExpenseRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_AddUpdExpense input);
    }
}
