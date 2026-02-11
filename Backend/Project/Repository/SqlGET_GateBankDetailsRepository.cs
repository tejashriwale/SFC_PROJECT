using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_GateBankDetailsRepository : IGET_GateBankDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GateBankDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_GateBankDetails>> getasync(GET_GateBankDetailsDomain input)
        {
            List<GET_GateBankDetails> result = new List<GET_GateBankDetails>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_GateBankDetails]('" + input.BANKID + "', '"+input.KEYWORD+"' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY date DESC";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        result.Add(new GET_GateBankDetails
                        {
                            BACCOUTNO = reader.GetString("BACCOUTNO"),
                            BANKID = reader.GetString("BANKID"),
                            BANKNAME = reader.GetString("BANKNAME"),
                            BBRANCHNAME = reader.GetString("BBRANCHNAME"),
                            BIFSCCODE = reader.GetString("BIFSCCODE"),
                            BSTATUS = reader.GetString("BSTATUS"),
                            BACOUNTANTNAME = reader.GetString("BACOUNTANTNAME"),
                            STATUS = reader.GetString("STATUS"),
                            BLAZAR = reader.GetString("BLAZAR"),
                            DATE = reader.GetString("DATE"),
                            

                        });
                    }
                }
            }
            return result;
        }

        public async Task<List<GET_GateBankDetails>> getkeyword(GET_GateBankDetailsDomain input)
        {
            List<GET_GateBankDetails> result = new List<GET_GateBankDetails>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_GateBankDetails]('" + input.BANKID + "' , '"+input.KEYWORD+"','" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY date DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_GateBankDetails
                        {
                            BACCOUTNO = reader.GetString("BACCOUTNO"),
                            BANKID = reader.GetString("BANKID"),
                            BANKNAME = reader.GetString("BANKNAME"),
                            BBRANCHNAME = reader.GetString("BBRANCHNAME"),
                            BIFSCCODE = reader.GetString("BIFSCCODE"),
                            BSTATUS = reader.GetString("BSTATUS"),
                            BACOUNTANTNAME = reader.GetString("BACOUNTANTNAME"),
                            STATUS = reader.GetString("STATUS") ,
                            BLAZAR = reader.GetString("BLAZAR"),
                            DATE = reader.GetString("DATE"),
                           

                        });
                    }
                }
            }
            return result;
        }
    }
}
