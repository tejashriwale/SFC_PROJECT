using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_SP_DeletePackingMasters_Repository
    {
        Task<List<ApiResponseMessage>>getasync(_SP_DeletePackingMasters_ input);
    }
}
