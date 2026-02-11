using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdSFamilyInformationRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdSFamilyInformation> input);
    }
}
