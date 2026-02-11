using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_FARMERRepository
    {
        Task<List<GET_FARMER>> getasync(GET_FARMERDomain input);
        Task<List<GET_FARMER>> getkeyword(GET_FARMERDomain input);
    }
}
