using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VyapariBillMasterAuctionRepository
    {
        Task<List<GET_VyapariBillMasterAuction>> getasync(GET_VyapariBillMasterAuctionDomain input);
    }
}
