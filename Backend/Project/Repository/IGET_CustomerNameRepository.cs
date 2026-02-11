using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CustomerNameRepository
    {
        Task<List<GET_CustomerName>> getasync(GET_CustomerNameDomain input);
    }
}
