using Project.Model.Domain;

namespace Project.Repository
{
    public interface IHSNGSTDetailsRepository
    {
        Task<List<HSNGSTDetails>>getasync(HSNDOMAIN hSNDOMAIN);
    }
}
