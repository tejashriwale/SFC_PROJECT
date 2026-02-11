using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CategoryDropdownInvRepository
    {
        Task<List<GET_CategoryDropdownInv>> getasync(GET_CategoryDropdownInvDomain input);
    }
}
