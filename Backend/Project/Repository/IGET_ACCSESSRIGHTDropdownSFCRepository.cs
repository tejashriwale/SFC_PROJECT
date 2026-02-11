using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_ACCSESSRIGHTDropdownSFCRepository
    {
        Task<List<GET_ACCSESSRIGHTDropdownSFC>> getasync(GET_ACCSESSRIGHTDropdownSFCDomain input);
    }
}
