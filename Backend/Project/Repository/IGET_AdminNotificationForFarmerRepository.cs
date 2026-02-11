using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_AdminNotificationForFarmerRepository
    {
        Task<List<GET_AdminNotificationForFarmer>> getasync(GET_AdminNotificationForFarmerDomain input);
        Task<List<GET_AdminNotificationForFarmer>> getkeyword(GET_AdminNotificationForFarmerDomain input);
    }
}
