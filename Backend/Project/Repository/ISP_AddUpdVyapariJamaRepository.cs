using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdVyapariJamaRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdVyapariJama> input);
    }
}
