using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdRokdaVyapariBharnaRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdRokdaVyapariBharna> input);
    }
}
