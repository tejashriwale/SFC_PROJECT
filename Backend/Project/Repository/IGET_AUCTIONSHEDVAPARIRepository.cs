using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_AUCTIONSHEDVAPARIRepository
    {
        Task<List<GET_AUCTIONSHEDVAPARI>>getasync(GET_AUCTIONSHEDVAPARIDomain input);
    }
}
