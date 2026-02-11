using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VYAPARISERVICERepository
    {
        Task<List<GET_VYAPARISERVICE>>getasync(GET_VYAPARISERVICEDomain  input);
    }
}
