using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_AuctionDataByTokenRepository : IGET_AuctionDataByTokenRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_AuctionDataByTokenRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_AuctionDataByToken>> getasync(GET_AuctionDataByTokenDomain input)
        {
            List<GET_AuctionDataByToken> result = new List<GET_AuctionDataByToken>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_AuctionDataByToken]('" + input.COMPANYID + "' , '" + input.DEPTID + "' , '" + input.TOKEN + "' , '" + input.DATE + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_AuctionDataByToken
                        {
                            APKID = reader.GetString("APKID"),
                            TOKEN = reader.GetString("TOKEN"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            DATE = reader.GetString("DATE"),
                            MAID = reader.GetString("MAID"),
                            SEVA = reader.GetString("SEVA"),
                            VILLAGE = reader.GetString("VILLAGE"),
                            HAMALI = reader.GetString("HAMALI"),
                            TOLAI = reader.GetString("TOLAI"),
                            TOTALJALI = reader.GetInt32("TOTALJALI"),
                            TOTALWEIGHT = reader.GetDouble("TOTALWEIGHT"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            ISCOMPLETED = reader.GetBoolean("ISCOMPLETED"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),
                            DPKID = reader.GetString("DPKID"),
                            VYAPARI = reader.GetString("VYAPARI"),
                            JAALI = reader.GetInt32("JAALI"),
                            WEIGHT = reader.GetString("WEIGHT"),
                            WEIGHTLESS = reader.GetDouble("WEIGHTLESS"),
                            RATE = reader.GetDouble("RATE"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            ISDELETEDM = reader.GetBoolean("ISDELETEDM"),
                            FMOBILE = reader.GetString("FMOBILE"),
                           
                           

                        });
                    }
                }
            }
            return result;
        }
    }
}
