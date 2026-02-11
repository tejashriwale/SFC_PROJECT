using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CropVehicleLoadingDataRepository
    {
        Task<List<GET_CropVehicleLoadingData>> getasync(GET_CropVehicleLoadingDataDomain input);
    }
}
