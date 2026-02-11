using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_Report_FarmerbillRepository
    {
        Task<List<GET_Report_Farmerbill>> getasync(GET_Report_FarmerbillDomain input);
    }
}
