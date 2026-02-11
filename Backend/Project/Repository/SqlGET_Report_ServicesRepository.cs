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
                command.CommandText = "SELECT * FROM [GET_Report_Services]('" +input.STARTDATE + "' , '" + input.ENDTDATE + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
