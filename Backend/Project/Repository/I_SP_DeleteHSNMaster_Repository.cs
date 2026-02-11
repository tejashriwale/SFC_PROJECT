using Project.Model.Domain;
using Project.Model.Model;

namespace Project.Repository
{
    public interface I_SP_DeleteHSNMaster_Repository
    {
        Task <List<ApiResponseMessage>> getasync(_SP_DeleteHSNMaster_ input);
    }
}
