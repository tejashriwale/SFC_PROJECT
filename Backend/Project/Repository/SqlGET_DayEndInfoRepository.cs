using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public class SqlGET_DayEndInfoRepository : IGET_DayEndInfoRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_DayEndInfoRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_DayEndInfo>> getasync(GET_DayEndInfoDomain input)
        {
            List<GET_DayEndInfo> list = new List<GET_DayEndInfo>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_DayEndInfo]('" + input.DATE + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_DayEndInfo
                        {
                            PendingTokens = reader.GetInt32("PendingTokens"),
                            PendingAuctionSheds = reader.GetInt32("PendingAuctionSheds"),
                            PendingReceipts = reader.GetInt32("PendingReceipts"),
                            LoggedInUsers = reader.GetInt32("LoggedInUsers"),
                            PEDINGVOUCHERS = reader.GetInt32("PEDINGVOUCHERS"),
                           

                        });
                    }
                }
            }
            return list;
        }
    }
}
