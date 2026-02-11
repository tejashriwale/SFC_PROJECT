using Project.Model;
using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SubCategoryDropdownRepository
    {
        Task<List<GET_CategoryDropdown>> getasync(GET_SubCategoryDropdownDomain input);
    }
}
