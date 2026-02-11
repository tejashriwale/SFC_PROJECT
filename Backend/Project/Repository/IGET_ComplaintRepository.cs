using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_ComplaintRepository
    {
        Task<List<GET_Complaint>> getasync(GET_ComplaintDomain input);
        Task<List<GET_Complaint>> getkeyword(GET_ComplaintDomain input);
    }
}
