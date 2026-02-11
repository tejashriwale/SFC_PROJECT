using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteParkingRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteParking input);
    }
}
