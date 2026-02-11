using Project.Model;
using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_GETSTOCKCNTRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_GETSTOCKCNT input);
    }
}
