using Project.Model.Domain;

namespace Project.Model.DTO
{
    public interface IGET_EMPLOYEERepository
    {
        Task<List<GET_EMPLOYEE>>getasync(GET_EMPLOYEEDomain input);
    }
}
