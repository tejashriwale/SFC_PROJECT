using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_DashBoardRateAuctionRepository : IGET_DashBoardRateAuctionRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_DashBoardRateAuctionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_DashBoardRateAuction>> getall()
        {
            List<GET_DashBoardRateAuction> list = new List<GET_DashBoardRateAuction>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_DashBoardRateAuction]()";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_DashBoardRateAuction
                        {
                            DPKID = reader.GetString("DPKID"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            RATE = reader.GetDouble("RATE"),
                            ITEMNM = reader.GetString("ITEMNM"),
                            RATE_TYPE = reader.GetString("RATE_TYPE"),
                            RATE_VALUE = reader.GetInt32("RATE_VALUE"),
 
                        });
                    }
                }
            }
            return list;
        }
    }
}
