using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_FARMERBILLRepository
    {
        Task<List<GET_FARMERBILL>> getasync(GET_FARMERBILLDomain input);
    }
}
