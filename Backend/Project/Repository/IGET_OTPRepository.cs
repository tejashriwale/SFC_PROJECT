using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_OTPRepository
    {
        Task<List<GET_OTP>> getasync(GET_OTPDomain input);
    }
}
