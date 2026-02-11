using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyaparijamaByVyapariDroDownRepository
    {
        Task<List<GET_VyaparijamaByVyapariDroDown>> getasync(GET_VyaparijamaByVyapariDroDownDomain input);
    }
}
