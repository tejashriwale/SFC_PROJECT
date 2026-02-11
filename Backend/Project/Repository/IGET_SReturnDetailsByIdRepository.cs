using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SReturnDetailsByIdRepository
    {
        Task<List<GET_SReturnDetailsById>>getasync(GET_SReturnDetailsByIdDomain input);
    }
}
