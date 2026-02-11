using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model;
using System.Data;

namespace Project.Repository
{
    public class SqlSP_TrialBalanceDataRepository : ISP_TrialBalanceDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlSP_TrialBalanceDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<SP_TrialBalanceData>> getasync(SP_TrialBalanceDataDomain input)
        {
            List<SP_TrialBalanceData> result = new List<SP_TrialBalanceData>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SP_TrialBalanceData";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@OrganizationID";
                parameter.Value = input.OrganizationID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@DivisionID";
                parameter.Value = input.DivisionID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@SDATE";
                parameter.Value = input.SDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@EDATE";
                parameter.Value = input.EDATE;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@LEVEL";
                parameter.Value = input.LEVEL;
                command.Parameters.Add(parameter);

                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                   while (await reader.ReadAsync())
                    {
                        result.Add(new SP_TrialBalanceData
                        {
                            MGRPID		 = reader.GetString("MGRPID"),
                            MGroupAccName = reader.GetString("MGroupAccName"),
                            MGroupAccNameE = reader.GetString("MGroupAccNameE"),
                            GRPID		 = reader.GetString("GRPID"),
                            GroupAccName	 = reader.GetString("GroupAccName"),
                            GroupAccNameE = reader.GetString("GroupAccNameE"),
                            ACCAID		 = reader.GetString("ACCAID"),
                            AccName		 = reader.GetString("AccName"),
                            AccNameE		 = reader.GetString("AccNameE"),
                            TotalAmount = reader.GetDouble("TotalAmount"),
                            CBStatus		 = reader.GetString("CBStatus"),
                            AccLevel		 = reader.GetString("AccLevel"),
                            ACCGID = reader.GetInt32("ACCGID"),
                         
                        });
                   }
                }
                return result;

            }
        }
    }
}
