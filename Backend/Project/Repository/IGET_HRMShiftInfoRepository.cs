using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_HRMShiftInfoRepository
    {
        Task<List<GET_HRMShiftInfo>>getasync(GET_HRMShiftInfoDomain input);

        Task<List<GET_HRMShiftInfo>> getkeyword(GET_HRMShiftInfoDomain input);
    }
}
