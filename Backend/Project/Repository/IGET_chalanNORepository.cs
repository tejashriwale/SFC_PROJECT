using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_chalanNORepository
    {
        Task<List<GET_chalanNO>> getasync(GET_chalanNODomain input);
    }
}
