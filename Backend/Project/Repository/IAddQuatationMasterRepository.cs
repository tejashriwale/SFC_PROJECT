using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Project.Model.Domain;

namespace Project.Repository
{
    public interface IAddQuatationMasterRepository
    {
        Task<List<ApiResponseMessage>>getasync(AddQuatationMaster quatationMaster);
    }
}
