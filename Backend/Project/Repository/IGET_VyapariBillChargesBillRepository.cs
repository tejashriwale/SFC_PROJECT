using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariBillChargesBillRepository
    {
        Task<List<GET_VyapariBillChargesBill>> getasync(GET_VyapariBillChargesBillDomain input);
    }
}
