using Project.Model.Domain;

namespace Project.Repository
{
    public interface IISP_AddVehicalDetailsAppRepositor
    {
        Task<List<ApiResponseMessage>> getasync(SP_AddVehicalDetailsApp input);
    }
}
