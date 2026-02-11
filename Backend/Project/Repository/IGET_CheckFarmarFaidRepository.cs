using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CheckFarmarFaidRepository
    {
        Task<List<GET_CheckFarmarFaid>> getasync(GET_CheckFarmarFaidDomain input);
    }
}
