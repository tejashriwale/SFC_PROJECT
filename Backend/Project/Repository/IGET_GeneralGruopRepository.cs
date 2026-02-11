using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_GeneralGruopRepository
    {
        Task<List<GET_GeneralGruop>>getasync(GET_GeneralGruopDomain input);
    }
}
