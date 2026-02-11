using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_StandingInstrcutionRepository
    {
        Task<List<GET_StandingInstrcution>> getasync(GET_StandingInstrcutionDomain input);
       
    }
}
