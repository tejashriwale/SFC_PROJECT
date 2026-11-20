using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_NewTokenRepository : IGET_NewTokenRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_NewTokenRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_NewToken>> getasync(GET_NewTokenDomain input)
        {
            List<GET_NewToken> result = new List<GET_NewToken>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_NewToken](@p0)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p0); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        result.Add(new GET_NewToken
                        {
                            CNT = reader.GetInt32("CNT"),
                            DATE = reader.GetString("DATE")
                        });
                    }
                }
            }
            return result;
        }
    }
}
