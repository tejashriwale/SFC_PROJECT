using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_SuggestPhoneNumberRepository
    {
        Task<List<GET_SuggestPhoneNumber>> getasync(GET_SuggestPhoneNumberDomain input);
        Task<List<GET_SuggestPhoneNumber>> getkeyword(GET_SuggestPhoneNumberDomain input);
    }
}
