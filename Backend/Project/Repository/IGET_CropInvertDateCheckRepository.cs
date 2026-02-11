using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CropInvertDateCheckRepository
    {
        Task<List<GET_CropInvertDateCheck>> getasync(GET_CropInvertDateCheckDomain input);
    }
}
