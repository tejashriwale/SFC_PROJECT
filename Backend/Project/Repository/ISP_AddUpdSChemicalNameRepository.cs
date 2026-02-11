using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdSChemicalNameRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdSChemicalName> input);
    }
}
