using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyapariBillMasterAuctionRepository : IGET_VyapariBillMasterAuctionRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariBillMasterAuctionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_VyapariBillMasterAuction>> getasync(GET_VyapariBillMasterAuctionDomain input)
        {
            List<GET_VyapariBillMasterAuction> result = new List<GET_VyapariBillMasterAuction>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_VyapariBillMasterAuction]('" + input.VBKID + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_VyapariBillMasterAuction
                        {
                            VBKID = reader.GetString("VBKID"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            DATE = reader.GetString("DATE"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            SHORTNAME = reader.GetString("SHORTNAME"),
                            COMPANYNAME = reader.GetString("COMPANYNAME"),
                            PHONE = reader.GetString("PHONE"),
                            AADHAR = reader.GetString("AADHAR"),
                            BANKNAME = reader.GetString("BANKNAME"),
                            ACCOUNTNUMBER = reader.GetString("ACCOUNTNUMBER"),
                            IFSCCODE = reader.GetString("IFSCCODE"),
                            ACCOUNTNAME = reader.GetString("ACCOUNTNAME"),
                            BRANCHNAME = reader.GetString("BRANCHNAME"),
                            TOTALJAALI = reader.GetInt32("TOTALJAALI"),
                            TOTALWEIGHT = reader.GetDouble("TOTALWEIGHT"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                            SEVA = reader.GetString("SEVA"),
                            TOTALLOSS = reader.GetDouble("TOTALLOSS"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            APKID = reader.GetString("APKID"),
                            ISDELETED = reader.GetBoolean("ISDELETED"),


                        });
                    }
                }
            }
            return result;
        }
    }
}
