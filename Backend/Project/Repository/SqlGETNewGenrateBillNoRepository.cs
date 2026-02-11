using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGETNewGenrateBillNoRepository : IGETNewGenrateBillNoRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGETNewGenrateBillNoRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GETNewGenrateBillNo>> getasync(GETNewGenrateBillNoDomain input)
        {
            List<GETNewGenrateBillNo> result = new List<GETNewGenrateBillNo>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GETNewGenrateBillNo]('"+input.COMPANYID+ "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
                {
                   while(await reader.ReadAsync())
                    {
                        result.Add(new GETNewGenrateBillNo
                        {
                            CNT = reader.GetInt32("CNT"),
                        });
                    }
                }
            }
            return result;
        }
    }
}
