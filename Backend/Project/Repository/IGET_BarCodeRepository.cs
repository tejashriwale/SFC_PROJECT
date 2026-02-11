using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_BarCodeRepository
    {
        Task<List<GET_BarCode>>getasync(GET_BarCodeDomain input);
    }
}
