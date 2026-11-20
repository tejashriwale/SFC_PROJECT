using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_Report_farmerRepository : IGET_Report_farmerRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_Report_farmerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_Report_farmer>> getasync(GET_Report_farmerDomain input)
        {
            List<GET_Report_farmer> result = new List<GET_Report_farmer>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_Report_farmer](@p0 , @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.STARTDATE) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.ENDTDATE) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_Report_farmer
                        {

                            AMOUNT = reader.GetDouble("AMOUNT"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            DATE = reader.GetString("DATE"),
                            JALI = reader.GetInt32("JALI"),
                            RATE = reader.GetDouble("RATE"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            TOTALCARETS = reader.GetDouble("TOTALCARETS"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            CROPLABEL = reader.GetString("CROPLABEL"),
                            TOTALCOST = reader.GetDouble("TOTALCOST"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            WEIGHTMINMAX = reader.GetString("WEIGHTMINMAX"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                            TOTALWEIGHT = reader.GetDouble("TOTALWEIGHT"),
                         
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            VILLAGENAME = reader.GetString("VILLAGENAME"),
                            BAID = reader.GetString("BAID"),
                        });
                    }
                }

            }
            return result;
        }
    }
}
