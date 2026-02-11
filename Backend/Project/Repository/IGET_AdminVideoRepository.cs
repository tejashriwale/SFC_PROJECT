using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_AdminVideoRepository
    {
        Task<List<GET_AdminVideo>>getasync(GET_AdminVideoDomain input);
        Task<List<GET_AdminVideo>>getkeyword(GET_AdminVideoDomain input);
    }
}
