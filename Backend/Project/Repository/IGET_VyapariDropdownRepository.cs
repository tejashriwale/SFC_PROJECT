using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariDropdownRepository
    {
        Task<List<GET_VyapariDropdown>>getasync(GET_VyapariDropdownDomain input);
    }
}
