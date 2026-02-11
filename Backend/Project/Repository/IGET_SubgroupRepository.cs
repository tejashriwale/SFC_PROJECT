using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SubgroupRepository
    {
        Task<List<GET_Subgroup>>getasync(GET_SubgroupDomain input);
    }
}
