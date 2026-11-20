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
                command.CommandText = "SELECT * FROM [GETNewGenrateBillNo](@p0 , @p1)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
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
