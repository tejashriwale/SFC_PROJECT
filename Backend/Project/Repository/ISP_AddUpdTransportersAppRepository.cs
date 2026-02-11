using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdTransportersAppRepository
    {
        Task<List<SP_AddUpdTransportersApp2>> getasync(SP_AddUpdTransportersApp input);
    }
}
