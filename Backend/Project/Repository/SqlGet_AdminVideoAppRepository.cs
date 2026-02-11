using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGet_AdminVideoAppRepository : IGet_AdminVideoAppRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGet_AdminVideoAppRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Get_AdminVideoApp>> GetAll()
        {
            List<Get_AdminVideoApp> list = new List<Get_AdminVideoApp>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [Get_AdminVideoApp]()";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new Get_AdminVideoApp
                        {
                            Vid = reader.GetString("Vid"),
                            Vtitle = reader.GetString("Vtitle"),
                            Video = reader.GetString("Video"),
                          

                        });
                    }
                }
            }
            return list;
        }
    }
}
