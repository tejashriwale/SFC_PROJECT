using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_PreCoolingRepository : IGET_PreCoolingRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PreCoolingRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_PreCooling>> getasync(GET_PreCoolingDomain input)
        {
            List<GET_PreCooling> list = new List<GET_PreCooling>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_PreCooling]('" + input.PCID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY DATE DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_PreCooling
                        {

                            PCID = reader.GetString("PCID"),
                            DATE = reader.GetString("DATE"),
                            RATE = reader.GetDouble("RATE"),
                            STATUS = reader.GetString("STATUS"),
                            DAYSFROM = reader.GetString("DAYSFROM"),
                            DAYSTO = reader.GetString("DAYSTO"),
                           

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_PreCooling>> getkeyword(GET_PreCoolingDomain input)
        {
            List<GET_PreCooling> list = new List<GET_PreCooling>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_PreCooling]('" + input.PCID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY DATE DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_PreCooling
                        {

                            PCID = reader.GetString("PCID"),
                            DATE = reader.GetString("DATE"),
                            RATE = reader.GetDouble("RATE"),
                            STATUS = reader.GetString("STATUS"),
                            DAYSFROM = reader.GetString("DAYSFROM"),
                            DAYSTO = reader.GetString("DAYSTO"),


                        });
                    }
                }
            }
            return list;
        }
    }
}
