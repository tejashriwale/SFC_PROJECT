using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_FARMERFROMCONTACTRepository
    {
        Task<List<GET_FARMERFROMCONTACT>> getasync(GET_FARMERFROMCONTACTDomain input);
    }
}
