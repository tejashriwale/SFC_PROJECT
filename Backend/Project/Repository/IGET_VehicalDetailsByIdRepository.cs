using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_VehicalDetailsByIdRepository 
    {
        Task<List<GET_VehicalDetailsById>>getasync(GET_VehicalDetailsByIdDomain input);
    }
}
