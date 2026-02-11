using Project.Model.Domain;

namespace Project.Repository
{
    public interface IISP_AddUpdVyapariBillMasterByBillRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdVyapariBillMasterByBill> input);
    }
}
