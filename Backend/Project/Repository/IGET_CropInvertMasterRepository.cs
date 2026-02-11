using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CropInvertMasterRepository
    {
        Task<List<GET_CropInvertMaster>> getasync(GET_CropInvertMasterDomain input);
        Task<List<GET_CropInvertMaster>> getkeyword(GET_CropInvertMasterDomain input);
    }
}
