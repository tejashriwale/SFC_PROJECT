using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_HSNCodeRepository
    {
        Task<List<GET_HSNCode>> getasync(HSNCodeDomain hSNCode);
    }
}
