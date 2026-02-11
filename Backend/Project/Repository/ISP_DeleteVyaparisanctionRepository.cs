using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteVyaparisanctionRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteVyaparisanction input);
    }
}
