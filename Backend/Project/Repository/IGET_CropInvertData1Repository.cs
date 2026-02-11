using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGET_CropInvertData1Repository
    {
        Task<List<GET_CropInvertData1>> getasync(GET_CropInvertData1Domain input);
    }
}
