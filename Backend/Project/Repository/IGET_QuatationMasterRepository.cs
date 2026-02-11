using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_QuatationMasterRepository
    {
       Task<List<GET_QuatationMaster>>getasync(GET_QuatationMasterDomain input);

       Task<List<GET_QuatationMaster>> getkeyword(GET_QuatationMasterDomain input);
    }
}
