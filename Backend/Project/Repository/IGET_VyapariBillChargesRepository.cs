using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariBillChargesRepository
    {
        Task<List<GET_VyapariBillCharges>> getsync(GET_VyapariBillChargesDomain input);
    }
}
