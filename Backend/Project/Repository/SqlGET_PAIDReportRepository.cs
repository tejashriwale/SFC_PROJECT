using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_PAIDReportRepository : IGET_PAIDReportRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PAIDReportRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ;
        }
        public async Task<List<GET_PAIDReport>> GetAsync(GET_PAIDReportDomain input)
        {
            List<GET_PAIDReport> list = new List<GET_PAIDReport>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PAIDReport](@p0 , @p1 ,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.STARTDATE) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.ENDTDATE) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_PAIDReport
                        {
                            DATE = reader.GetString("DATE"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            FNAME = reader.GetString("FNAME"),
                            VILLAGENAME = reader.GetString("VILLAGENAME"),
                            VEHICLENO = reader.GetString("VEHICLENO"),
                            TOTALCOST = reader.GetDouble("TOTALCOST"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            CASHAMOUNT = reader.GetDouble("CASHAMOUNT"),
                            CHEQUEAMOUNT = reader.GetDouble("CHEQUEAMOUNT"),
                            ONLINEAMOUNT = reader.GetDouble("ONLINEAMOUNT"),
                            TOTAL = reader.GetDouble("TOTAL"),
                            BAKIRAKKAM = reader.GetDouble("BAKIRAKKAM")


                        });
                    }
                }
            }
            return list;
        }
    }
}
