using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_NewTokenRepository
    {
        Task<List<GET_NewToken>>getasync(GET_NewTokenDomain input);
    }
}
