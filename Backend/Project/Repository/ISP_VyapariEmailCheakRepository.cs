using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_VyapariEmailCheakRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_VyapariEmailCheak input);
    }
}
