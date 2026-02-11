using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_ST_Implication_DayRepository
    {
        Task<List<GET_ST_Implication_Day>> GetAll();
    }
}
