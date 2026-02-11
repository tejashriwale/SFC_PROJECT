using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGET_ServiceChargeRepository
    {
        Task<List<GET_ServiceCharge>>getasync(GET_ServiceChargeDomain input);
        Task<List<GET_ServiceCharge>>getkeyword(GET_ServiceChargeDomain input);
    }
}
