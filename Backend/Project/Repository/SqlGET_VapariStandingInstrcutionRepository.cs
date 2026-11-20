using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VapariStandingInstrcutionRepository : IGET_VapariStandingInstrcutionRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VapariStandingInstrcutionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_VapariStandingInstrcution>> getsync(GET_VapariStandingInstrcutionDomain input)
        {
            List<GET_VapariStandingInstrcution> list = new List<GET_VapariStandingInstrcution> ();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_VapariStandingInstrcution](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SHID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_VapariStandingInstrcution
                        {
                          
                            RATETYPE = reader.GetString("RATETYPE"),
                            
                            NEWRATE = reader.GetDouble("NEWRATE"),
                            
                            PKID = reader.GetString("PKID"),
                            SERVICETYPE = reader.GetString("SERVICETYPE"),
                        });
                    }
                }
            }
            return list;

        }
    }
}
