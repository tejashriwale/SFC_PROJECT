using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public interface IGET_TyreCustomerPartnersRepository
    {
        Task<List<GET_TyreCustomerPartners>> getasync(GET_TyreCustomerPartnersDomain input);
        Task<List<GET_TyreCustomerPartners>> getkeyword(GET_TyreCustomerPartnersDomain input);
    }
}
