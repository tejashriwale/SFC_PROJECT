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
                command.CommandText = "SELECT * FROM [GET_StockCount]('" + input.COMPANYID + "' , '" + input.DEPTID + "'  , '" + input.PAID + "')";
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
