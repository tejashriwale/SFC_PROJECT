using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_HRM_HolidayRepository
    {
        Task<List<GET_HRM_Holiday>>getasync(GET_HRM_HolidayDomain input);

        Task<List<GET_HRM_Holiday>> getkeyword(GET_HRM_HolidayDomain input);
    }
}
