using Project.Model.Domain;

namespace Project.Repository
{
    public interface IGET_PackingMaterialRepository
    {
        Task<List<GET_PackingMaterial>> getasync(GET_PackingMaterialDomain input);
        Task<List<GET_PackingMaterial>> getkeyword(GET_PackingMaterialDomain input);
    }
}
