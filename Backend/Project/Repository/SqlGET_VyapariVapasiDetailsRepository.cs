using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_VyapariVapasiDetailsRepository : IGET_VyapariVapasiDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariVapasiDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
      
        public async Task<List<GET_VyapariVapasiDetails>> getasync(GET_VyapariVapasiDetailsDomain input)
        {

            List<GET_VyapariVapasiDetails> result = new List<GET_VyapariVapasiDetails>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_VyapariVapasiDetails](@p0  , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VPID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_VyapariVapasiDetails
                        {
                            VPID = reader.GetString("VPID"),
                            VSAID = reader.GetString("VSAID"),
                            VSDAID = reader.GetString("VSDAID"),
                            DUE_DAY = reader.GetString("DUE_DAY"),
                            TO_DAY = reader.GetString("TO_DAY"),
                           
                            VAPASI = reader.GetString("VAPASI"),

                   

                        });
                    }
                }
            }
            return result;
        }
    }
}
