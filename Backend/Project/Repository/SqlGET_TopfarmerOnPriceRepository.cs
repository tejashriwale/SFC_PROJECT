using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_TopfarmerOnPriceRepository : IGET_TopfarmerOnPriceRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_TopfarmerOnPriceRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_TopfarmerOnPrice>> getasync(GET_TopfarmerOnPriceDomain input)
        {
            List<GET_TopfarmerOnPrice> list = new List<GET_TopfarmerOnPrice>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select * from [GET_TopfarmerOnPrice]('" + input.APKID + "','" + input.FROMDATE + "','" + input.TODATE + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_TopfarmerOnPrice
                        {
                            APKID = reader.GetString("APKID"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            FNAME = reader.GetString("FNAME"),
                           
                        });
                    }
                }
            }
            return list;
        }
    }
}
