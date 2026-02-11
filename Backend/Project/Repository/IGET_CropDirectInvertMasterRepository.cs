using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CropDirectInvertMasterRepository
    {
        Task<List<GET_CropDirectInvertMaster>> getasync(GET_CropDirectInvertMasterDomain input);
        Task<List<GET_CropDirectInvertMaster>> getkeyword(GET_CropDirectInvertMasterDomain input);
    }
}
