using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_RokadVyapariSchemeNameRepository
    {
        Task<List<GET_RokadVyapariSchemeName>> getasync(GET_RokadVyapariSchemeNameDomain input);
    }
}
