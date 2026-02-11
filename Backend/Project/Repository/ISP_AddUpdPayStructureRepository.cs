using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdPayStructureRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_AddUpdPayStructure input);
    }
}
