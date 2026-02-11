using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_DistrictRepository
    {
        Task<List<GET_District>> GetAll();
    }
}
