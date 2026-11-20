using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_Report_VyapariBillRepository : IGET_Report_VyapariBillRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_Report_VyapariBillRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_Report_VyapariBill>> getasync(GET_Report_VyapariBillDomain input)
        {
            List<GET_Report_VyapariBill> result = new List<GET_Report_VyapariBill>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_Report_VyapariBill](@p0 , @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.STARTDATE) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.ENDTDATE) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_Report_VyapariBill
                        {

                           
                            BILLNO = reader.GetInt32("BILLNO"),
                            DATE = reader.GetString("DATE"),
                            VNAME = reader.GetString("VNAME"),
                            RATE = reader.GetDouble("RATE"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            LIMITS = reader.GetBoolean("LIMITS"),
                            PHONE = reader.GetString("PHONE"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                            TOTALWEIGHT = reader.GetDouble("TOTALWEIGHT"),

                        });
                    }
                }

            }
            return result;
        }
    }
}
