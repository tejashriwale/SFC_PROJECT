using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_TopVyapariOnCratesRepository
    {
        Task<List<GET_TopVyapariOnCrates>> getasync(GET_TopVyapariOnCratesDomain input);
        Task<List<GET_TopVyapariOnCrates>> getkeyword(GET_TopVyapariOnCratesDomain input);
    }
}
