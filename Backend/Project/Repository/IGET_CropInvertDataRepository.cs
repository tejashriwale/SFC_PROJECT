using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CropInvertDataRepository
    {
        Task<List<GET_CropInvertData>> getasync(GET_CropInvertDataDomain input);
        Task<List<GET_CropInvertData>> getkeyword(GET_CropInvertDataDomain input);
    }
}
