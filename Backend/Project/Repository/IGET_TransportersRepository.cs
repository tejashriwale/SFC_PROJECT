using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_TransportersRepository
    {
        Task<List<GET_Transporters>>getasync(GET_TransportersDomain input);
        Task<List<GET_Transporters>>getkeyword(GET_TransportersDomain input);
    }
}
