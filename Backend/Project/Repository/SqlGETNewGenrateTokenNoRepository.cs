using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGETNewGenrateTokenNoRepository : IGETNewGenrateTokenNoRepository
    {
        private ApplicationDbContext dbContext;
        public SqlGETNewGenrateTokenNoRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GETNewGenrateTokenNo>> getasync(GETNewGenrateTokenNoDomain input)
        {
            List<GETNewGenrateTokenNo> list = new List<GETNewGenrateTokenNo>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GETNewGenrateTokenNo](@p0 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p0); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GETNewGenrateTokenNo
                        {
                            CNT = reader.GetInt32("CNT"),
                           
                        });
                    }
                }
            }
            return list;
        }
    }
}
