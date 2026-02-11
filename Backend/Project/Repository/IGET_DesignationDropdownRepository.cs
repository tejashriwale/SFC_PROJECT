using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_DesignationDropdownRepository
    {
        Task<List<GET_DesignationDropdown>> getasync(GET_DesignationDropdownDomain input);
    }
}
