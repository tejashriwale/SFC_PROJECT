using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_LedgerDataRepository : IGET_LedgerDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_LedgerDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ;
        }
        public async Task<List<GET_LedgerData>> getasync(GET_LedgerDataDomain input)
        {
            List<GET_LedgerData> result = new List<GET_LedgerData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_LedgerData](@p0  ,@p1  , @p2 , @p3 , @p4)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.OrganizationID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DivisionID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.ACCAID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.SDATE) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.EDATE) ?? ""); command.Parameters.Add(command_p4); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_LedgerData
                        {
                            DATE		 = reader.GetDateTime("DATE"),
                            DRAMT	 = reader.GetDouble("DRAMT"),
                            CRAMT	 = reader.GetDouble("CRAMT"),
                            DRCR		 = reader.GetString("DRCR"),
                            ACCAID	 = reader.GetString("ACCAID"),
                            GLRPID	 = reader.GetString("GLRPID"),
                            MGRPID	 = reader.GetString("MGRPID"),
                            SGRPID	 = reader.GetString("SGRPID"),
                            GENRALLAGER	 = reader.GetString("GENRALLAGER"),
                            SUBLAGER		 = reader.GetString("SUBLAGER"),
                            LAGER = reader.GetString("LAGER"),
                            NARR = reader.GetString("NARR")



                        });
                    }
                }
            }
            return result;
        }
    }
}
