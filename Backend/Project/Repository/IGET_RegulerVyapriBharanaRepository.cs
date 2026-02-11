using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_RegulerVyapriBharanaRepository
    {
        Task<List<GET_RegulerVyapriBharana>> getasync(GET_RegulerVyapriBharanaDomain input);
        
    }
}
