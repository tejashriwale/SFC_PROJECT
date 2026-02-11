using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SFertilizersNameRepository
    {
        Task<List<GET_SFertilizersName>> getasync(GET_SFertilizersNameDomain input);
    }
}
