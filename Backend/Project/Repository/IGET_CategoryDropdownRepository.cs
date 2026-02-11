using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CategoryDropdownRepository
    {
        Task<List<GET_CategoryDropdown>>getasync(GET_CategoryDropdownDomain input);
    }
}
