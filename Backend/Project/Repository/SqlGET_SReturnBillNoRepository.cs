using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_SReturnBillNoRepository : IGET_SReturnBillNoRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SReturnBillNoRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SReturnBillNo>> getasync(GET_SReturnBillNoDomai input)
        {
            List<GET_SReturnBillNo> list = new List<GET_SReturnBillNo>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM[GET_SReturnBillNo](@p0)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.SBNO) ?? ""); command.Parameters.Add(command_p0); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SReturnBillNo
                        {
                          SBNO = reader.GetString("SBNO"),
                          SBBILLNO = reader.GetString("SBBILLNO"),
                          AVAILABLE_FOR_RETURN = reader.GetInt32("AVAILABLE_FOR_RETURN"),
                          BillQuantity = reader.GetString("BillQuantity"),
                          ReturnQuantity = reader.GetString("ReturnQuantity"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
