using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_FarmerSearchBYAadhaarRepository
    {
        Task<List<GET_FarmerSearchBYAadhaar>>getasync(GET_FarmerSearchBYAadhaarDomain input);
    }
}
