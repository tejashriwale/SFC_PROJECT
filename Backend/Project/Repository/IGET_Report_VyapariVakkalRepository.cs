using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_Report_VyapariVakkalRepository
    {
        Task<List<GET_Report_VyapariVakkal>> getasync(GET_Report_VyapariVakkalDomain input);
    }
}
