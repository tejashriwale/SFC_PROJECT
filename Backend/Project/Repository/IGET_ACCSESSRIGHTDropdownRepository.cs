using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_ACCSESSRIGHTDropdownRepository
    {
        Task<List<GET_ACCSESSRIGHTDropdown>> GetAll();
    }
}
