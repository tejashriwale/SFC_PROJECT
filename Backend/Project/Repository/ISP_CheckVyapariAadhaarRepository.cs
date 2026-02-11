using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_CheckVyapariAadhaarRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_CheckVyapariAadhaar input);
    }
}
