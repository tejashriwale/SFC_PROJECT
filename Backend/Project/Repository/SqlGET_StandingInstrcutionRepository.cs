using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_StandingInstrcutionRepository : IGET_StandingInstrcutionRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_StandingInstrcutionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_StandingInstrcution>> getasync(GET_StandingInstrcutionDomain input)
        {
            List<GET_StandingInstrcution> list = new List<GET_StandingInstrcution> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_StandingInstrcution](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SIID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_StandingInstrcution
                        {
                            SCID = reader.GetString("SCID"),
                            SHID = reader.GetString("SHID"),
                            NEWRATE = reader.GetDouble("NEWRATE"),
                            SIID = reader.GetString("SIID"),
                            RATE = reader.GetDouble("RATE"),
                        });
                    }
                }
            }
            return list;
        }

       
        
    }
}
