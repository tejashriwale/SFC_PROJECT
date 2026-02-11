using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_ProfitLossDataRepository : IGET_ProfitLossDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ProfitLossDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_ProfitLossData>> getasync(GET_ProfitLossDataDomain input)
        {
            List<GET_ProfitLossData> list = new List<GET_ProfitLossData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_ProfitLossData]('" + input.OrganizationID + "','" + input.DivisionID + "' , '" + input.SDATE + "' , '" + input.EDATE + "')";
                await dbContext.Database.OpenConnectionAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_ProfitLossData
                        {
                            AMT = reader.GetDouble("AMT"),
                            ACCAID = reader.GetString("ACCAID"),
                            DEPTID = reader.GetString("DEPTID"),
                            ACCOUNTNAME = reader.GetString("ACCOUNTNAME"),
                            SGRPID	 = reader.GetString("SGRPID"),
                            SUBGROUP = reader.GetString("SUBGROUP"),
                 
                        });
                    }
                }
            }
            return list;
        }
    }
}
