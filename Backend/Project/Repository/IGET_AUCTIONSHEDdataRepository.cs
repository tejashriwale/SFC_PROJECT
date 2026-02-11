using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_AUCTIONSHEDdataRepository
    {
        Task<List<GET_AUCTIONSHEDdata>>getasync(GET_AUCTIONSHEDdataDomain input);
    }
}
