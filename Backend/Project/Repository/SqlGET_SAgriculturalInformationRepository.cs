using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_SAgriculturalInformationRepository : IGET_SAgriculturalInformationRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SAgriculturalInformationRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SAgriculturalInformation>> getasync(GET_SAgriculturalInformationDomain input)
        {
            List<GET_SAgriculturalInformation> result = new List<GET_SAgriculturalInformation>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_SAgriculturalInformation](@p0 , @p1 ,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SRAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DSAID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_SAgriculturalInformation
                        {
                           SRAID = reader.GetString("SRAID"), 
                            DSAID = reader.GetString("DSAID"),
                            SURVEYNUMBER = reader.GetString("SURVEYNUMBER"),
                            CLUSTERNUMBER = reader.GetString("CLUSTERNUMBER"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            LANDAREA = reader.GetString("LANDAREA"),
                            LANDTYPE = reader.GetString("LANDTYPE"),
                            LANDNAME = reader.GetString("LANDNAME"),

                        });
                    }
                }
            }
            return result;
        }
    }
}
