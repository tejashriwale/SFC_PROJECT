using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariSlabMasterDataRepository
    {
        Task<List<GET_VyapariSlabMasterData>> getasync(GET_VyapariSlabMasterDataDomain input);
    }
}
