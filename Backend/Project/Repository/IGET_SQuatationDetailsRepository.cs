using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SQuatationDetailsRepository
    {
        Task<List<GET_SQuatationDetails>>getasync(GET_SQuatationDetailsDomain input);
    }
}
