using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SFamilyInformationRepository
    {
        Task<List<GET_SFamilyInformation>> getasync(GET_SFamilyInformationDomain input);
    }
}
