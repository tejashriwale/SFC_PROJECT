using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_AuctionDataBillRepository : IGET_AuctionDataBillRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_AuctionDataBillRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_AuctionDataBill>> getasync(GET_AuctionDataBillDomain input)
        {
            List<GET_AuctionDataBill> list = new List<GET_AuctionDataBill>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_AuctionDataBill](@p0 , @p1 , @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.APKID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_AuctionDataBill
                        {
                            APKID = reader.GetString("APKID"),
                            TOKEN = reader.GetString("TOKEN"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            DATE = reader.GetString("DATE"),
                            MAID = reader.GetString("MAID"),
                            VILLAGE = reader.GetString("VILLAGE"),
                            SEVA = reader.GetString("SEVA"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            FIFSCCODE = reader.GetString("FIFSCCODE"),
                            TOTALJALI = reader.GetInt32("TOTALJALI"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            FNAME = reader.GetString("FNAME"),
                            VYAPARI = reader.GetString("VYAPARI"),
                            JAALI = reader.GetInt32("JAALI"),
                            FBANKNAME = reader.GetString("FBANKNAME"),
                            WEIGHTLESS = reader.GetDouble("WEIGHTLESS"),
                            RATE = reader.GetDouble("RATE"),
                            TOKNNOG = reader.GetString("TOKNNOG"),
                            WEIGHT = reader.GetString("WEIGHT"),
                            FACCOUNTNO = reader.GetString("FACCOUNTNO"),
                            FCONTACTNO = reader.GetString("FCONTACTNO"),
                            VEHNO = reader.GetString("VEHNO"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            FACCONAME = reader.GetString("FACCONAME"),
                            CARETS_COUNT = reader.GetString("CARETS_COUNT"),


                        });
                    }
                }
            }
            return list;
        }
    }
}
