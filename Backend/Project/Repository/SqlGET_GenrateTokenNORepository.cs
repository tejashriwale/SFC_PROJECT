using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_GenrateTokenNORepository : IGET_GenrateTokenNORepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GenrateTokenNORepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_GenrateTokenNO>> getasync(GET_GenrateTokenNODomain input)
        {
            List<GET_GenrateTokenNO> result = new List<GET_GenrateTokenNO>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_GenrateTokenNO]('" + input.DATE + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        result.Add(new GET_GenrateTokenNO
                        {
                            CNT = reader.GetInt32("CNT")
                        });
                    }
                }
            }
            return result;
        }
    }
}
