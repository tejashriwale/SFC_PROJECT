using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_QuatationSRNoRepository
    {
        Task<List<GET_QuatationSRNo>> getasync(GET_QuatationSRNoDomain input);
    }
}
