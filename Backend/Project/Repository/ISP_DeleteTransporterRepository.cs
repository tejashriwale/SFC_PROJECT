using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_DeleteTransporterRepository
    {
        Task<List<ApiResponseMessage>> getasync(SP_DeleteTransporter input);
    }
}
