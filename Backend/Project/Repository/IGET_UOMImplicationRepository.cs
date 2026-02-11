using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_UOMImplicationRepository
    {
        Task<List<GET_UOMImplication>>getasync(GET_UOMImplicationDomain input);
    }
}
