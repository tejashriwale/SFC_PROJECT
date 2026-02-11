using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_RECEIPTDataBillByTokenRepository
    {
        Task<List<GET_RECEIPTDataBillByToken>>getasync(GET_RECEIPTDataBillByTokenDomain input);
    }
}
