using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_RECEIPTDataBillRepository : IGET_RECEIPTDataBillRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_RECEIPTDataBillRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_RECEIPTDataBill>> getasync(GET_RECEIPTDataBillDomain input)
        {
           List<GET_RECEIPTDataBill> result = new List<GET_RECEIPTDataBill>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM  [GET_RECEIPTDataBill]('" + input.COMPANYID + "' , '" + input.DEPTID + "' , '" + input.BAID + "' , '"+input.DATE+"')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        result.Add(new GET_RECEIPTDataBill
                        {
                            ACCNAME = reader.GetString("ACCNAME"),
                            ACCNO = reader.GetString("ACCNO"),
                            AADHARNO = reader.GetString("AADHARNO"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            BAID = reader.GetString("BAID"),
                            BANKNAME = reader.GetString("BANKNAME"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            CROPTYPE = reader.GetString("CROPTYPE"),
                            DATE = reader.GetString("DATE"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            FACCONAME = reader.GetString("FACCONAME"),
                            FBANKNAME = reader.GetString("FBANKNAME"),
                            FCONTACTNO = reader.GetString("FCONTACTNO"),
                            FNAME = reader.GetString("FNAME"),
                            FIFSCCODE = reader.GetString("FIFSCCODE"),
                            FULLNAME = reader.GetString("FULLNAME"),
                            IFSCCODE = reader.GetString("IFSCCODE"),
                            JALI = reader.GetInt32("JALI"),
                            MAID = reader.GetString("MAID"),
                            PHONENUMBER = reader.GetString("PHONENUMBER"),
                            RATE = reader.GetDouble("RATE"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            SEVA = reader.GetString("SEVA"),
                            STATUS = reader.GetString("STATUS"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                            TOTALCARETS = reader.GetDouble("TOTALCARETS"),
                            TOTALCOST = reader.GetDouble("TOTALCOST"),
                            TOTALWEIGHT = reader.GetDouble("TOTALWEIGHT"),
                            TRNTID = reader.GetString("TRNTID"),
                            BDAID = reader.GetString("BDAID"),
                            VEHICLENO = reader.GetString("VEHICLENO"),
                            VEHICALRENT = reader.GetDouble("VEHICALRENT"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            VILLAGENAME = reader.GetString("VILLAGENAME"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            WEIGHTMINMAX = reader.GetString("WEIGHTMINMAX"),
                            TOKNNOG = reader.GetString("TOKNNOG"),
                            VEHNO = reader.GetString("VEHNO"),
                            FACCOUNTNO = reader.GetString("FACCOUNTNO"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            CARETS_COUNT = reader.GetString("CARETS_COUNT"),
                            ISCHECKED = reader.GetString("ISCHECKED"),
                           VNAME = reader.GetString("VNAME")

                        });
                    }
                }

            }
            return result;
        }
    }
}
