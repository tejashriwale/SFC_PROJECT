using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_Report_ServicesRepository
    {
        Task<List<GET_Report_Services>> getasync(GET_Report_ServicesDomain domain);
        
    }
}
