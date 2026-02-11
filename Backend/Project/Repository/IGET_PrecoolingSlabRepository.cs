using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PrecoolingSlabRepository
    {
        Task<List<GET_PrecoolingSlab>> getasync(GET_PrecoolingSlabDomain input);
    }
}
