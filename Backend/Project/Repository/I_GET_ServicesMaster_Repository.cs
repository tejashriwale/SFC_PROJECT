using Project.Model.Domain;

namespace Project.Repository
{
    public interface I_GET_ServicesMaster_Repository
    {
        Task<List<_GET_ServicesMaster_>> getasync(getByIdInput input);

        Task<List<_GET_ServicesMaster_>> Get_GET_ServicesAsync(getByIdInput input);

        Task<List<_GET_ServicesMaster_>> searchasync(getByIdInput input);
    }

   
}
