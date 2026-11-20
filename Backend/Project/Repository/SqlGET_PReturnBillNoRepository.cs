using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_PReturnBillNoRepository : IGET_PReturnBillNoRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PReturnBillNoRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_PReturnBillNo>> getasync(GET_PReturnBillNoDomain input)
        {
            List<GET_PReturnBillNo> list = new List<GET_PReturnBillNo>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PReturnBillNo](@p0)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PBNO) ?? ""); command.Parameters.Add(command_p0); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_PReturnBillNo
                        {
                          PBNO = reader.GetString("PBNO")
                        });
                    }
                }
            }
            return list;
        }
    }
}
