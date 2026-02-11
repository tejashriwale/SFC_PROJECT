using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_HIGHESTFRUITRATEEAuctionReposirory
    {
        Task<List<GET_HIGHESTFRUITRATEEAuction>> getasync(GET_HIGHESTFRUITRATEEAuctionDomain input);
    }
}
