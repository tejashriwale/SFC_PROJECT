using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGET_FarmerFamilyDetailRepository
    {
        Task<List<GET_FarmerFamilyDetail>> getasync(GET_FarmerFamilyDetailDomain input);
    }
}
