using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGSTUPDATERepository
    {
        Task<List<ApiResponseMessage>> getasync(GSTUPDATE gSTUPDATE);
    }
}
