using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SCHEMENAMERepository
    {
        Task<List<GET_SCHEMENAME>> getasync(GET_SCHEMENAMEDomain input);
    }
}
