using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_ST_Implication_ServiceChargeRepository
    {
        Task<List<GET_ST_Implication_ServiceCharge>> GetAll();
    }
}
