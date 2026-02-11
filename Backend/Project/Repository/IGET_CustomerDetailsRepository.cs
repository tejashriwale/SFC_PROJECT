using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGET_CustomerDetailsRepository
    {
      Task<List<GET_CustomerDetails>>getasync(CustomerDetailsDomain input);
    }
}
