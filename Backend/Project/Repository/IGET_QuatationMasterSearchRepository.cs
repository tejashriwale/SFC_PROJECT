using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_QuatationMasterSearchRepository
    {
        Task<List<GET_QuatationMasterSearch>> getasync(GET_QuatationMasterSearchDomain gET_Quatation);
    }
}
