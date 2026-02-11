using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_ST_Implication_ShopLOCRepository
    {
        Task<List<GET_ST_Implication_ShopLOC>> GetAll();
    }
}
