using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_challan_from_Return_masterRepository
    {
        Task<List<GET_challan_from_Return_master>> getasync(GET_challan_from_Return_masterDomain input);
    }
}
