using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_FarmerFromFamilyDetailsRepository
    {
        Task<List<GET_FarmerFromFamilyDetails>>getasync(GET_FarmerFromFamilyDetailsDomain input);
    }
}
