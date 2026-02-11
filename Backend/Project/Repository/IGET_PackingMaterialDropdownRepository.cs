using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PackingMaterialDropdownRepository
    {
        Task<List<GET_PackingMaterialDropdown>> getall();
    }
}
