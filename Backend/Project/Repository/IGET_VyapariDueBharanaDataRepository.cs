using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariDueBharanaDataRepository
    {
        Task<List<GET_VyapariDueBharanaData>> getasync(GET_VyapariDueBharanaDataDomain input);
    }
}
