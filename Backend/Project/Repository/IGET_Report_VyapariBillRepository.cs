using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_Report_VyapariBillRepository
    {
        Task<List<GET_Report_VyapariBill>> getasync(GET_Report_VyapariBillDomain input);
    }
}
