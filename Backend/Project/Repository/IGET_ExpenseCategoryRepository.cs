using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_ExpenseCategoryRepository
    {
        Task<List<GET_ExpenseCategory>>getasync(GET_ExpenseCategoryDomain input);

        Task<List<GET_ExpenseCategory>> getkeyword(GET_ExpenseCategoryDomain input);
    }
}
