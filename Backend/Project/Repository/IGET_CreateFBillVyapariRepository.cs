using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CreateFBillVyapariRepository
    {
        Task<List<GET_CreateFBillVyapari>> getasync(GET_CreateFBillVyapariDomain input);
    }
}
