using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_ActiveVyapariDetailsRepository
    {
        Task<List<GET_ActiveVyapariDetails>> getasync(GET_ActiveVyapariDetailsDomain input);
    }
}
