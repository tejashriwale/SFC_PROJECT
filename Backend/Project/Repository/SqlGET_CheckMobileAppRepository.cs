using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CheckMobileAppRepository : IGET_CheckMobileAppRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CheckMobileAppRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CheckMobileApp>> getasync(GET_CheckMobileAppDomain input)
        {
            List<GET_CheckMobileApp> list = new List<GET_CheckMobileApp>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CheckMobileApp](@p0)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.TCANTACTNO) ?? ""); command.Parameters.Add(command_p0); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_CheckMobileApp
                        {
                            TAID = reader.GetString("TAID"),
                            TBIRTHDATE = reader.GetString("TBIRTHDATE"),
                            TCANTACTNO = reader.GetString("TCANTACTNO"),
                            TNAME = reader.GetString("TNAME"),
                            VEHICALNO = reader.GetString("VEHICALNO"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
