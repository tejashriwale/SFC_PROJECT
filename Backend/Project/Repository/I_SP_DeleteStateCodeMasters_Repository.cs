using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_SP_DeleteStateCodeMasters_Repository
    {
        Task<List<ApiResponseMessage>>getasync(_SP_DeleteStateCodeMasters_ input);
    }
}
