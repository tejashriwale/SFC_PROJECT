using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PayStructureRepository
    {
        Task<List<GET_PayStructure>>getasync(GET_PayStructureDomain input);
    }
}
