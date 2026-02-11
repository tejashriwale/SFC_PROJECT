using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_HRMDesignationNameRepository
    {
        Task<List<GET_HRMDesignationName>>getasync(GET_HRMDesignationNameDomain input);
    }
}
