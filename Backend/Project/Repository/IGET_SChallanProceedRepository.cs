using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SChallanProceedRepository
    {
        Task<List<GET_SChallanProceed>> getasync(GET_SChallanProceedDomain input);
    }
}
