using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_RECEIPTMasterAUCTIONRepository 
    {
        Task<List<GET_RECEIPTMasterAUCTION>>getasync(GET_RECEIPTMasterAUCTIONDomain input);
    }
}
