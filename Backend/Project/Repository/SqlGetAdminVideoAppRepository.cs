using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGetAdminVideoAppRepository : IGetAdminVideoAppRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGetAdminVideoAppRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GetAdminVideoApp>> getasync(GetAdminVideoAppDomain input)
        {
            List<GetAdminVideoApp> result = new List<GetAdminVideoApp>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GetAdminVideoApp]('" + input.PAXID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GetAdminVideoApp
                        {
                            Vid = reader.GetString("Vid"),
                            Vtitle = reader.GetString("Vtitle"),
                            Video = reader.GetString("Video"),
                         



                        });
                    }
                }
            }
            return result;
        }
    }
}
