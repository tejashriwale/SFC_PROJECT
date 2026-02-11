using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PlotDetailsByIdRepository
    {
        Task<List<GET_PlotDetailsById>>getasync(GET_PlotDetailsByIdDomain input);
    }
}
