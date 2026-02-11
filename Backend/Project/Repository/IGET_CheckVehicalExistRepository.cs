using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CheckVehicalExistRepository
    {
        Task<List<GET_CheckVehicalExist>> getasync(GET_CheckVehicalExistDomain input);
    }
}
