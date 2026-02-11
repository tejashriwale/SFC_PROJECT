using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGETItemDropdownRepository
    {
        Task<List<GETItemDropdown>>getasync(GETItemDropdownDomain input);
    }
}
