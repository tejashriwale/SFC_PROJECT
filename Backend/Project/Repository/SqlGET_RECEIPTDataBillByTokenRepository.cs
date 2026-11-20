using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_RECEIPTDataBillByTokenRepository : IGET_RECEIPTDataBillByTokenRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_RECEIPTDataBillByTokenRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_RECEIPTDataBillByToken>> getasync(GET_RECEIPTDataBillByTokenDomain input)
        {
            List<GET_RECEIPTDataBillByToken> list = new List<GET_RECEIPTDataBillByToken>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_RECEIPTDataBillByToken](@p0 , @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.BTOKANNO) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_RECEIPTDataBillByToken
                        {
                            AADHARNO = reader.GetString("AADHARNO"),
                            SEVA = reader.GetString("SEVA"),
                            STATUS = reader.GetString("STATUS"),
                            FIFSCCODE = reader.GetString("FIFSCCODE"),
                            IFSCCODE = reader.GetString("IFSCCODE"),
                            TOTALCARETS = reader.GetDouble("TOTALCARETS"),
                            DATE = reader.GetString("DATE"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            TOTALCOST = reader.GetDouble("TOTALCOST"),
                            ACCNAME = reader.GetString("ACCNAME"),
                            ACCNO = reader.GetString("ACCNO"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            BAID = reader.GetString("BAID"),
                            BANKNAME = reader.GetString("BANKNAME"),
                            BDAID = reader.GetString("BDAID"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            CROPTYPE = reader.GetString("CROPTYPE"),
                            FACCONAME = reader.GetString("FACCONAME"),
                            FACCOUNTNO = reader.GetString("FACCOUNTNO"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            FBANKNAME = reader.GetString("FBANKNAME"),
                            MAID = reader.GetString("MAID"),
                            FCONTACTNO = reader.GetString("FCONTACTNO"),
                            FNAME = reader.GetString("FNAME"),
                            FULLNAME = reader.GetString("FULLNAME"),
                            JALI = reader.GetInt32("JALI"),
                            PHONENUMBER = reader.GetString("PHONENUMBER"),
                            RATE = reader.GetDouble("RATE"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                            TOTALWEIGHT = reader.GetDouble("TOTALWEIGHT"),
                            TRNTID = reader.GetString("TRNTID"),
                            VEHICALRENT = reader.GetDouble("VEHICALRENT"),
                            VEHICLENO = reader.GetString("VEHICLENO"),
                            VILLAGENAME = reader.GetString("VILLAGENAME"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            WEIGHTMINMAX = reader.GetString("WEIGHTMINMAX"),
                            TOKNNOG = reader.GetString("TOKNNOG"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            VEHNO = reader.GetString("VEHNO"),
                            CARETS_COUNT = reader.GetString("CARETS_COUNT"),
                            ISCHECKED = reader.GetString("ISCHECKED"),
                            ISVYAPARIVERIFIED = reader.GetBoolean("ISVYAPARIVERIFIED"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
