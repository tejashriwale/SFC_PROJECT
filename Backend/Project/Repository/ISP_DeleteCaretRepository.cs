using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteCaretRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteCaretDomain input);
    }
}
