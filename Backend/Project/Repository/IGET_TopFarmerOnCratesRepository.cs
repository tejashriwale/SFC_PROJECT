using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_TopFarmerOnCratesRepository
    {
        Task<List<GET_TopFarmerOnCrates>> getasync(GET_TopFarmerOnCratesDomain input);
        Task<List<GET_TopFarmerOnCrates>> getkeyword(GET_TopFarmerOnCratesDomain input);
    }
}
