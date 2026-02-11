using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteVisitorDetailRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteVisitorDetail input);
    }
}
