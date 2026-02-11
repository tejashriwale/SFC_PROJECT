using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdCaretRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddUpdCaret input);
    }
}
