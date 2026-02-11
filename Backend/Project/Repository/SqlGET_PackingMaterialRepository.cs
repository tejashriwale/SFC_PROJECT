using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_PackingMaterialRepository : IGET_PackingMaterialRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PackingMaterialRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_PackingMaterial>> getasync(GET_PackingMaterialDomain input)
        {
            List<GET_PackingMaterial> list = new List<GET_PackingMaterial>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT TOp 20 * FROM [GET_PackingMaterial] ('" + input.MAID + "' ,  '" + input.KEYWORD +  "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_PackingMaterial
                        { 
                            MAID = reader.GetString("MAID"),
                            COUNT = reader.GetString("COUNT"),
                            MTYPE = reader.GetString("MTYPE"),
                            UOM = reader.GetString("UOM"),
                            MSIZE = reader.GetString("MSIZE")

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_PackingMaterial>> getkeyword(GET_PackingMaterialDomain input)
        {
            List<GET_PackingMaterial> list = new List<GET_PackingMaterial>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_PackingMaterial] ('" + input.MAID + "' ,  '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_PackingMaterial
                        {
                            MAID = reader.GetString("MAID"),
                            COUNT = reader.GetString("COUNT"),
                            MTYPE = reader.GetString("MTYPE"),
                            UOM = reader.GetString("UOM"),
                            MSIZE = reader.GetString("MSIZE")

                        });
                    }
                }
            }
            return list;
        }
    }
}
