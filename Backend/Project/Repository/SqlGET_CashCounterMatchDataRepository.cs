using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CashCounterMatchDataRepository : IGET_CashCounterMatchDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CashCounterMatchDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CashCounterMatchData>> getasync(GET_CashCounterMatchDataDomain input)
        {
            List<GET_CashCounterMatchData> result = new List<GET_CashCounterMatchData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_CashCounterMatchData](@p0 , @p1 , @p2 , @p3)ORDER BY STATUS,SYSDT DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_CashCounterMatchData
                        {
                            PKID = reader.GetString("PKID"),
                            FAID = reader.GetString("FAID"),
                            BAID = reader.GetString("BAID"),
                            TOKENNO = reader.GetString("TOKENNO"),
                            BILLNO = reader.GetString("BILLNO"),
                            TARIKH = reader.GetString("TARIKH"),
                            FULLNAME = reader.GetString("FULLNAME"),
                            VEHICLENO = reader.GetString("VEHICLENO"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            BILLAMOUNT = reader.GetDouble("BILLAMOUNT"),
                            CASHAMOUNT = reader.GetDouble("CASHAMOUNT"),
                            CHEQUEAMOUNT = reader.GetDouble("CHEQUEAMOUNT"),
                            ONLINEAMOUNT = reader.GetDouble("ONLINEAMOUNT"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            STATUS = reader.GetBoolean("STATUS"),
                            FACCONAME = reader.GetString("FACCONAME"),
                            FBANKNAME = reader.GetString("FBANKNAME"),
                            FACCOUNTNO = reader.GetString("FACCOUNTNO"),
                            FIFSCCODE = reader.GetString("FIFSCCODE"),
                            RECEIPT_BAID = reader.GetString("RECEIPT_BAID"),
                            CHEQUEAPPROVED = reader.GetString("CHEQUEAPPROVED"),
                            CHEQUENO = reader.GetString("CHEQUENO"),
                            CHEQUEDATE = reader.GetString("CHEQUEDATE"),
                            ISCHECKED = reader.GetBoolean("ISCHECKED"),
                            SYSDT = reader.GetDateTime("SYSDT")

                        });
                    }
                }
            }
            return result;
        }

        public async Task<List<GET_CashCounterMatchData>> getkeyword(GET_CashCounterMatchDataDomain input)
        {
            List<GET_CashCounterMatchData> result = new List<GET_CashCounterMatchData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CashCounterMatchData](@p0 , @p1 , @p2 , @p3)ORDER BY STATUS,SYSDT DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_CashCounterMatchData
                        {
                            PKID = reader.GetString("PKID"),
                            FAID = reader.GetString("FAID"),
                            BAID = reader.GetString("BAID"),
                            TOKENNO = reader.GetString("TOKENNO"),
                            BILLNO = reader.GetString("BILLNO"),
                            TARIKH = reader.GetString("TARIKH"),
                            FULLNAME = reader.GetString("FULLNAME"),
                            VEHICLENO = reader.GetString("VEHICLENO"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            BILLAMOUNT = reader.GetDouble("BILLAMOUNT"),
                            CASHAMOUNT = reader.GetDouble("CASHAMOUNT"),
                            CHEQUEAMOUNT = reader.GetDouble("CHEQUEAMOUNT"),
                            ONLINEAMOUNT = reader.GetDouble("ONLINEAMOUNT"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            STATUS = reader.GetBoolean("STATUS"),
                            ISCHECKED = reader.GetBoolean("ISCHECKED"),
                            FACCONAME = reader.GetString("FACCONAME"),
                            FBANKNAME = reader.GetString("FBANKNAME"),
                            FACCOUNTNO = reader.GetString("FACCOUNTNO"),
                            FIFSCCODE = reader.GetString("FIFSCCODE"),
                            RECEIPT_BAID = reader.GetString("RECEIPT_BAID"),
                            CHEQUEAPPROVED = reader.GetString("CHEQUEAPPROVED"),
                            CHEQUENO = reader.GetString("CHEQUENO"),
                            CHEQUEDATE = reader.GetString("CHEQUEDATE"),
                            SYSDT = reader.GetDateTime("SYSDT"),
                           

                        });
                    }
                }
            }
            return result;
        }
    }
}
