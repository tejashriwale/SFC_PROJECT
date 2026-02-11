using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_FarmerDetailsRepository
    {
        Task<List<GET_FarmerDetails>> getasync(GET_FarmerDetailsDomain input);
    }
}
