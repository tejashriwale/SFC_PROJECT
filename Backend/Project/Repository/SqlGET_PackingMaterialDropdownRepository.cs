using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_PackingMaterialDropdownRepository : IGET_PackingMaterialDropdownRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PackingMaterialDropdownRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_PackingMaterialDropdown>> getall()
        {
            List<GET_PackingMaterialDropdown> list = new List<GET_PackingMaterialDropdown>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PackingMaterialDropdown]()";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        list.Add(new GET_PackingMaterialDropdown
                        {
                            PackingMaterial = reader.GetString("PackingMaterial"),
                            MAID = reader.GetString("MAID"),
                            COUNT = reader.GetString("COUNT"),
                            MSIZE = reader.GetString("MSIZE"),
                           
                        });
                    }
                }
            }
            return list;
        }
    }
}
