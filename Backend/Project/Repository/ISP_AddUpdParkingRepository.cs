using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdParkingRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdParking input);
    }
}
