using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_AUCTIONSHEDVAPARIRepository : IGET_AUCTIONSHEDVAPARIRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_AUCTIONSHEDVAPARIRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_AUCTIONSHEDVAPARI>> getasync(GET_AUCTIONSHEDVAPARIDomain input)
        {
            List<GET_AUCTIONSHEDVAPARI> list = new List<GET_AUCTIONSHEDVAPARI>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_AUCTIONSHEDVAPARI](@p0 , @p1 , @p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VBKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.APKID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    list.Add(new GET_AUCTIONSHEDVAPARI
                    {
                        SEVA = reader.GetString("SEVA"),
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
                        TOTALJAALI = reader.GetDouble("TOTALJAALI"),
                        TOTALWEIGHT = reader.GetDouble("TOTALWEIGHT"),
                        TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                        TOTALLOSS = reader.GetDouble("TOTALLOSS"),
                        REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                        APKID = reader.GetString("APKID"),

                    });
                }
            }
            return list;
        }
    }
}
