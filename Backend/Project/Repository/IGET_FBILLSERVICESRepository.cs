using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_FBILLSERVICESRepository
    {
        Task<List<GET_FBILLSERVICES>> getasync(GET_FBILLSERVICESDomain input);
    }
}
