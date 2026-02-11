using Project.Model.Domain;

namespace Project.Repository
{
    public interface ISP_AddUpdHRMHolidayRepository
    {
        Task<List<ApiResponseMessage>>getasync(SP_AddUpdHRMHoliday sP_AddUpdHRMHoliday);
    }
}
