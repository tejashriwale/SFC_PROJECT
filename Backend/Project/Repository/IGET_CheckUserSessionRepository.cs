using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_CheckUserSessionRepository
    {
        Task<List<GET_CheckUserSession>> getasync(GET_CheckUserSessionDomain input);
    }
}
