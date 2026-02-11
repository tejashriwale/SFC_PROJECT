using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_Report_farmerRepository
    {
        Task<List<GET_Report_farmer>> getasync(GET_Report_farmerDomain input);
    }
}
