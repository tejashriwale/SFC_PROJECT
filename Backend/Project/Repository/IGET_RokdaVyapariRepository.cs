using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_RokdaVyapariRepository
    {
        Task<List<GET_RokdaVyapari>> getasync(GET_RokdaVyapariDomain input);
        Task<List<GET_RokdaVyapari>> getkeyword(GET_RokdaVyapariDomain input);
    }
}
