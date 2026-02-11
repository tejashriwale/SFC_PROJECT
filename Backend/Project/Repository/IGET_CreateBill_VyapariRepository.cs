using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CreateBill_VyapariRepository
    {
        Task<List<GET_CreateBill_Vyapari>> getasync(GET_CreateBill_VyapariDomain input);
    }
}
