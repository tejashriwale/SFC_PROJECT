using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_DelarRateRepository
    {
        Task<List<GET_DelarRate>> getasync(GET_DelarRateDomain input);
        Task<List<GET_DelarRate>> getkeyword(GET_DelarRateDomain input);
    }
}
