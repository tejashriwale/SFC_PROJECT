using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariJamaMasterRepository
    {
        Task<List<GET_VyapariJamaMaster>> getasync(GET_VyapariJamaMasterDomain input);
        Task<List<GET_VyapariJamaMaster>> getkeyword(GET_VyapariJamaMasterDomain input);
    }
}
