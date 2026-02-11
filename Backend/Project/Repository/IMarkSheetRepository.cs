using Project.Model.Domain;

namespace Project.Repository
{
    public interface IMarkSheetRepository
    {
        Task<List<MarkSheet>> GetAllAsyn();
    }
}
