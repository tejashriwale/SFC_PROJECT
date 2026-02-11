using Project.Model.Domain;

namespace Project.Repository
{
    public interface IDirectInvertRepository
    {
        Task<List<ApiResponseMessage>> addAsync(DirectInvert input);
    }
}
