using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_VyapariAuctionViewRepository : IGET_VyapariAuctionViewRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariAuctionViewRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_VyapariAuctionView>> getasync(GET_VyapariAuctionViewDomain input)
        {
            List<GET_VyapariAuctionView> list = new List<GET_VyapariAuctionView>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_VyapariAuctionView]('" + input.PKID + "' , '"+input.KEYWORD+"')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VyapariAuctionView
                        {
                            PKID = reader.GetString("PKID"),
                            VPAID = reader.GetString("VPAID"),
                            UserID = reader.GetString("UserID"),
                            DATETIME = reader.GetString("DATETIME"),
                            APKID = reader.GetString("APKID"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            VMOBLIE = reader.GetString("VMOBLIE"),
                            DATE = reader.GetString("DATE"),
                            FNAME = reader.GetString("FNAME"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                            TOTALJALI = reader.GetInt32("TOTALJALI"),
                            TOKEN = reader.GetInt32("TOKEN"),

                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_VyapariAuctionView>> getkeyword(GET_VyapariAuctionViewDomain input)
        {
            List<GET_VyapariAuctionView> list = new List<GET_VyapariAuctionView>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_VyapariAuctionView]('" + input.PKID + "' , '"+input.KEYWORD+"')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VyapariAuctionView
                        {
                            PKID = reader.GetString("PKID"),
                            VPAID = reader.GetString("VPAID"),
                            UserID = reader.GetString("UserID"),
                            DATETIME = reader.GetString("DATETIME"),
                            APKID = reader.GetString("APKID"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            VMOBLIE = reader.GetString("VMOBLIE"),
                            DATE = reader.GetString("DATE"),
                            FNAME = reader.GetString("FNAME"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                            TOTALJALI = reader.GetInt32("TOTALJALI"),
                            TOKEN = reader.GetInt32("TOKEN"),

                        });
                    }
                }
            }
            return list;
        }
    }
    
}
