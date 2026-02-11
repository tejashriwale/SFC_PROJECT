using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SAgriculturalInformationRepository
    {
        Task<List<GET_SAgriculturalInformation>> getasync(GET_SAgriculturalInformationDomain input);
    }
}
