using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VapariStandingInstrcutionRepository
    {
        Task<List<GET_VapariStandingInstrcution>>getsync(GET_VapariStandingInstrcutionDomain input);
    }
}
