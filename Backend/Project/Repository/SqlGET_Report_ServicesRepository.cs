using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_Report_ServicesRepository : IGET_Report_ServicesRepository
    {
     private readonly ApplicationDbContext dbContext;
    public SqlGET_Report_ServicesRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<List<GET_Report_Services>> getasync(GET_Report_ServicesDomain input)
        {
            List<GET_Report_Services> result = new List<GET_Report_Services>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_Report_Services](@p0 , @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.STARTDATE) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.ENDTDATE) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_Report_Services
                        {

                            SCID = reader.GetString("SCID"),
                            RATEAPPLICABLE = reader.GetString("RATEAPPLICABLE"),
                           
                            SERVICETYPE = reader.GetString("SERVICETYPE"),
                            RATETYPE = reader.GetString("RATETYPE"),
                            LEJAR = reader.GetString("LEJAR"),
                            RATE = reader.GetDouble("RATE"),
                            ISAPPROVAL = reader.GetBoolean("ISAPPROVAL"),
                            RTYPE  = reader.GetString("RTYPE"),
                            STYPE  = reader.GetString("STYPE"),
                            RAPPLI = reader.GetString("RAPPLI"),
                            LEJARL = reader.GetString("LEJARL"),
                            DATE = reader.GetString("DATE"),



                        });
                    }
                }

            }
            return result;
        }
    }
}
