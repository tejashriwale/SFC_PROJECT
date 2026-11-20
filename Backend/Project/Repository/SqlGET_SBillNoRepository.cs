using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_SBillNoRepository : IGET_SBillNoRepository
    {
        private readonly ApplicationDbContext dbContext;    
        public SqlGET_SBillNoRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SBillNo>> getasync(GET_SBillNoDomain input)
        {
            List<GET_SBillNo> result = new List<GET_SBillNo>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SBillNo](@p0)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SBBILLNO) ?? ""); command.Parameters.Add(command_p0); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_SBillNo
                        {
                          BILLNO = reader.GetString("BILLNO")
                        });
                    }
                }

            }
            return result;
        }
    }
}
