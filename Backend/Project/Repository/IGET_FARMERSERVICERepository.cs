using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_FARMERSERVICERepository
    {
        Task<List<GET_FARMERSERVICE>>getasync(GET_FARMERSERVICEDomain input);
    }
}
