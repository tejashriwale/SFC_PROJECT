using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_ExpenseRepository
    {
        Task<List<GET_Expense>>getasync(GET_ExpenseDomain input);

        Task<List<GET_Expense>> getkeyword(GET_ExpenseDomain input);
    }
}
