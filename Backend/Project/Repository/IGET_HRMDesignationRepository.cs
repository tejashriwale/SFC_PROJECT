using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_HRMDesignationRepository
    {
        Task<List<GET_HRMDesignation>>getasync(GET_HRMDesignationDomain input);

        Task<List<GET_HRMDesignation>> getkeyword(GET_HRMDesignationDomain input);
    }
}
