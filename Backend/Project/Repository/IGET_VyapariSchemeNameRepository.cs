using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariSchemeNameRepository
    {
        Task<List<GET_VyapariSchemeName>> getasync(GET_VyapariSchemeNameDomain input);
    }
}
