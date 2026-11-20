using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_StockCountRepository : IGET_StockCountRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_StockCountRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_StockCount>> getasync(GET_StockCountDomain input)
        {
            List<GET_StockCount> list = new List<GET_StockCount>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_StockCount](@p0 , @p1  , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.PAID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_StockCount
                        {
                            PAID = reader.GetString("PAID"),
                            TotalQuantity = reader.GetInt64("TotalQuantity"),
                            PRODUCT = reader.GetString("PRODUCT"),
                            UOM = reader.GetString("UOM"),
                          
                        });
                    }
                }
            }
            return list;
        }
    }
}
