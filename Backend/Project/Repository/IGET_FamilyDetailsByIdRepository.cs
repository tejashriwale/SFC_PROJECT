using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_FamilyDetailsByIdRepository
    {
        Task<List<GET_FamilyDetailsById>>getasync(GET_FamilyDetailsByIdDomain input);
    }
}
