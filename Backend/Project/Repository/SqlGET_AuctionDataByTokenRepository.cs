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
                command.CommandText = "SELECT * FROM [GET_AuctionDataByToken](@p0 , @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.TOKEN) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p3); }
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
