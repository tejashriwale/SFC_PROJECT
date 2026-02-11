using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdSAgriculturalInformationRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdSAgriculturalInformation> input);
    }
}
