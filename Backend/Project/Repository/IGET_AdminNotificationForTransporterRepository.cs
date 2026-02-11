using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_AdminNotificationForTransporterRepository
    {
        Task<List<GET_AdminNotificationForTransporter>> getasync(GET_AdminNotificationForTransporterDomain input);
        Task<List<GET_AdminNotificationForTransporter>> getkeyword(GET_AdminNotificationForTransporterDomain input);
    }
}
