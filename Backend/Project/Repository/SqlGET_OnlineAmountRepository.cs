using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_OnlineAmountRepository : IGET_OnlineAmountRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_OnlineAmountRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_OnlineAmount>> getasync(GET_OnlineAmountDomain input)
        {
            List<GET_OnlineAmount> list = new List<GET_OnlineAmount>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_OnlineAmount](@p0 ,@p1 , @p2 , @p3 ,@p4 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.BAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p4); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_OnlineAmount
                        {
                            BAID = reader.GetString("BAID"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            DATE = reader.GetString("DATE"),
                            MAID = reader.GetString("MAID"),
                            FNAME = reader.GetString("FNAME"),
                            PHONENUMBER = reader.GetString("PHONENUMBER"),
                            ACCNO = reader.GetString("ACCNO"),
                            IFSCCODE = reader.GetString("IFSCCODE"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            PKID = reader.GetString("PKID"),
                            ONLINEAMOUNT = reader.GetDouble("ONLINEAMOUNT"),
                           


                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_OnlineAmount>> getkeyword(GET_OnlineAmountDomain input)
        {
            List<GET_OnlineAmount> list = new List<GET_OnlineAmount>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_OnlineAmount](@p0 ,@p1 , @p2 , @p3 ,@p4 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.BAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p4); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_OnlineAmount
                        {
                            BAID = reader.GetString("BAID"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            DATE = reader.GetString("DATE"),
                            MAID = reader.GetString("MAID"),
                            FNAME = reader.GetString("FNAME"),
                            PHONENUMBER = reader.GetString("PHONENUMBER"),
                            ACCNO = reader.GetString("ACCNO"),
                            IFSCCODE = reader.GetString("IFSCCODE"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            PKID = reader.GetString("PKID"),
                            ONLINEAMOUNT = reader.GetDouble("ONLINEAMOUNT"),



                        });
                    }
                }
            }
            return list;
        }
    }
}
