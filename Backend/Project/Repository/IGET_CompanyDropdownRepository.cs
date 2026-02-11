using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CompanyDropdownRepository
    {
        Task<List<GET_CompanyDropdown>> getall();
    }
}
