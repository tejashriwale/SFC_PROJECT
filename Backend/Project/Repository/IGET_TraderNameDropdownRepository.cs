using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_TraderNameDropdownRepository
    {
        Task<List<GET_TraderNameDropdown>> getasync(GET_TraderNameDropdownDomain input);
    }
}
