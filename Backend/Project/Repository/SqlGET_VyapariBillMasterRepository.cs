using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_VyapariBillMasterRepository : IGET_VyapariBillMasterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariBillMasterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_VyapariBillMaster>> getasync(GET_VyapariBillMasterDomain input)
        {
            List<GET_VyapariBillMaster> result = new List<GET_VyapariBillMaster>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_VyapariBillMaster](@p0 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.BILLNO) ?? ""); command.Parameters.Add(command_p0); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_VyapariBillMaster
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
