using Project.Model.Domain;

namespace Project.Repository
{
    public interface IAddUpdPOrderMasterrepository
    {
        Task<List<ApiResponseMessage>> getasync(AddUpdPOrderMaster pOrderMaster);

    }
}
