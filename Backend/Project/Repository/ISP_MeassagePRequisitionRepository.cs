using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_MeassagePRequisitionRepository
    {
        Task<List<ApiResponseMessage>>getaync(SP_MeassagePRequisitionDomain input);
    }
}
