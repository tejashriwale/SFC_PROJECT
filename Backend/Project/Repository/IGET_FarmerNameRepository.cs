using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_FarmerNameRepository
    {
        Task<List<GET_FarmerName>>getasync(GET_FarmerNameDomain input);
    }
}
