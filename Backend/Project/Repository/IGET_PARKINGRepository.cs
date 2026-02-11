using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PARKINGRepository
    {
        Task<List<GET_PARKING>> GetAsync(GET_PARKINGDomain input);
        Task<List<GET_PARKING>> getkeyword(GET_PARKINGDomain input);
    }
}
