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
                command.CommandText = "SELECT Top 20 * FROM [GET_GateBankDetails](@p0, @p1 , @p2 , @p3)ORDER BY date DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.BANKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
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
                command.CommandText = "SELECT  * FROM [GET_GateBankDetails](@p0 , @p1,@p2 , @p3)ORDER BY date DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.BANKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
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
