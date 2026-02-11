using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SingleRowCropInvertDetailRepository
    {
        Task<List<GET_SingleRowCropInvertDetail>> getasync(GET_SingleRowCropInvertDetailDomain input);
    }
}
