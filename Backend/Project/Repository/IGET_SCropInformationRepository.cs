using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SCropInformationRepository
    {
        Task<List<GET_SCropInformation>> getasync(GET_SCropInformationDomain input);
    }
}
