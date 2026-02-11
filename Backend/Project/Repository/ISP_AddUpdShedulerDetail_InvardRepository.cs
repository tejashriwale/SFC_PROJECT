using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdShedulerDetail_InvardRepository
    {
        Task<List<ApiResponseMessage>> getasync(List<SP_AddUpdShedulerDetail_Invard> input);
    }
}
