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
                command.CommandText = "SELECT  * FROM [GET_ProfitLossData](@p0,@p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.OrganizationID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DivisionID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.SDATE) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.EDATE) ?? ""); command.Parameters.Add(command_p3); }
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
