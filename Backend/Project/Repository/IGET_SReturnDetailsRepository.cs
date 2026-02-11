using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SReturnDetailsRepository
    {
        Task<List<GET_SReturnDetails>> getasync(GET_SReturnDetailsDomain input);
    }
}
