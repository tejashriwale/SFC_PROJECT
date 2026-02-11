using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CategoryMasterDataRepository : IGET_CategoryMasterDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CategoryMasterDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CategoryMasterData>> getasync(GET_CategoryMasterDataDomain input)
        {
            List<GET_CategoryMasterData> list = new List<GET_CategoryMasterData>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_CategoryMasterData]('" + input.CTAID + "' , '" + input.CTTYPE + "' ,'" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_CategoryMasterData
                        {
                            CATEGORYNAME = reader.GetString("CATEGORYNAME"),
                            CTID = reader.GetString("CTID"),
                            CTAID = reader.GetString("CTAID"),
                            CTSUBCATGORYID = reader.GetString("CTSUBCATGORYID"),
                            CSTATUS = reader.GetBoolean("CSTATUS"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_CategoryMasterData>> getkeyword(GET_CategoryMasterDataDomain input)
        {
            List<GET_CategoryMasterData> list = new List<GET_CategoryMasterData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CategoryMasterData]('" + input.CTAID + "' , '" + input.CTTYPE + "' ,'" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CategoryMasterData
                        {
                            CATEGORYNAME = reader.GetString("CATEGORYNAME"),
                            CTID = reader.GetString("CTID"),
                            CTAID = reader.GetString("CTAID"),
                            CTSUBCATGORYID = reader.GetString("CTSUBCATGORYID"),
                            CSTATUS = reader.GetBoolean("CSTATUS"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
