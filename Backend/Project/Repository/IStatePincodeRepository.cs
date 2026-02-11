using Project.Model.Domain;

namespace Project.Repository
{
    public interface IStatePincodeRepository
    {
        Task<List<StatePincode>>getasync(StatePincodeDomain statePincodeDomain);
    }
}
