using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_NotifyTransporterOnTokenGenerationRepository
    {
        Task<List<GET_NotifyTransporterOnTokenGeneration>> getasync(GET_NotifyTransporterOnTokenGenerationDomain input);
    }
}
