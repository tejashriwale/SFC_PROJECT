using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CheckGateEntryVerifyRepository : IGET_CheckGateEntryVerifyRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CheckGateEntryVerifyRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CheckGateEntryVerify>> getasync(GET_CheckGateEntryVerifyDomain input)
        {
            List<GET_CheckGateEntryVerify> result = new List<GET_CheckGateEntryVerify>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CheckGateEntryVerify]('" + input.DPKID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        result.Add(new GET_CheckGateEntryVerify
                        {
                            DPKID = reader.GetString("DPKID"),
                            TOKEN = reader.GetString("TOKEN")
                        });
                    }
                }
            }
            return result;
        }
    }
}
