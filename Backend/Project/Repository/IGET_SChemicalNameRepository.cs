using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SChemicalNameRepository
    {
        Task<List<GET_SChemicalName>> getasync(GET_SChemicalNameDomain input);
    }
}
