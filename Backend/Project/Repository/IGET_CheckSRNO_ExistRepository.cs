using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CheckSRNO_ExistRepository
    {
        Task<List<GET_CheckSRNO_Exist>> getasync(GET_CheckSRNO_ExistDomain input);
    }
}
