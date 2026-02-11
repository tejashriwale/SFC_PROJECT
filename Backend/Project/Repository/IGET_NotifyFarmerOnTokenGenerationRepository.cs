using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_NotifyFarmerOnTokenGenerationRepository
    {
        Task<List<GET_NotifyFarmerOnTokenGeneration>> getasync(GET_NotifyFarmerOnTokenGenerationDomain input);
    }
}
