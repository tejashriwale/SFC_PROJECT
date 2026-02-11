using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariSchemeDetailsRepository
    {
        Task<List<GET_VyapariSchemeDetails>> getasync(GET_VyapariSchemeDetailsDomain input);
    }
}
