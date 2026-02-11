using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGET_GetRTGSRepository
    {
        Task<List<GET_GetRTGS>> getasync(GET_GetRTGSDomain input);
        Task<List<GET_GetRTGS>> getkeyword(GET_GetRTGSDomain input);
    }
}
