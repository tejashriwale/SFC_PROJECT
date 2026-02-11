using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGetItemImageRepository
    {
        Task<List<GetItemImage>> getasync(GetItemImageDomain input);
    }
}
