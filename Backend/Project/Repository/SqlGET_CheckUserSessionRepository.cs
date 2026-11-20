using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CheckUserSessionRepository : IGET_CheckUserSessionRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CheckUserSessionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CheckUserSession>> getasync(GET_CheckUserSessionDomain input)
        {
            List<GET_CheckUserSession> result = new List<GET_CheckUserSession>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CheckUserSession](@p0 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.UAID) ?? ""); command.Parameters.Add(command_p0); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_CheckUserSession
                        {
                            UAID = reader.GetString("UAID"),
                            UEMAILADDRESS = reader.GetString("UEMAILADDRESS"),
                            UPASSWORD = reader.GetString("UPASSWORD"),
                            ISLOGIN = reader.GetBoolean("ISLOGIN"),
                        });
                    }
                }
            }
            return result;
        }
    }
}
