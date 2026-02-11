using Project.Model.Domain;

namespace Project.Repository
{
    public interface IAddUpdCompanyRepository
    {
        Task<List<ApiResponseMessage>>getassync(AddUpdCompany addUpdCompany);
    }
}
