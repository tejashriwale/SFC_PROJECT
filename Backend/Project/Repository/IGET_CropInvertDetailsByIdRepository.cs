using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CropInvertDetailsByIdRepository
    {
        Task<List<GET_CropInvertDetailsById>> getasync(GET_CropInvertDetailsByIdDomain  input);
    }
}
