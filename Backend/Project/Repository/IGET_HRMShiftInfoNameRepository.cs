using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_HRMShiftInfoNameRepository
    {
        Task<List<GET_HRMShiftInfoName>>getasync(GET_HRMShiftInfoNameDomain input);
    }
}
