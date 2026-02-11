using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_Report_VyapariRepository
    {
        Task<List<GET_Report_Vyapari>> getasync(GET_Report_VyapariDomain input);
    }
}
