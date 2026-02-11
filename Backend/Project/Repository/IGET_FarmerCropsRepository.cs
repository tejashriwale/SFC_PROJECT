using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_FarmerCropsRepository
    {
        Task<List<GET_FarmerCrops>> getasync(GET_FarmerCropsDomain input);
    }
}
