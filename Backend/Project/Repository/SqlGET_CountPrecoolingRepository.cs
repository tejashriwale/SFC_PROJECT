using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CountPrecoolingRepository : IGET_CountPrecoolingRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CountPrecoolingRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_CountPrecooling>> getasync(GET_CountPrecoolingDomain input)
        {
            List<GET_CountPrecooling> list = new List<GET_CountPrecooling>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CountPrecooling]('" + input.startDate + "', '" + input.endDate + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CountPrecooling
                        {
                            PCID = reader.GetString("PCID"),
                            DATE = reader.GetString("DATE"),
                            DAYSFROM = reader.GetString("DAYSFROM"),
                            DAYSTO = reader.GetString("DAYSTO"),
                            RATE = reader.GetDouble("RATE"),
                            STATUS = reader.GetString("STATUS"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
