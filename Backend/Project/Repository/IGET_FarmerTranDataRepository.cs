using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_FarmerTranDataRepository
    {
        Task<List<GET_FarmerTranData>> getasync(GET_FarmerTranDataDomain input);
    }
}
