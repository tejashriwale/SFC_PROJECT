using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CropDirectInvertDetailRepository
    {
        Task<List<GET_CropDirectInvertDetail>> getasync(GET_CropDirectInvertDetailDomain input);
    }
}
