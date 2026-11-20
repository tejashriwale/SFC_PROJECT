using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CashCounterRepository : IGET_CashCounterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CashCounterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CashCounter>> getasync(GET_CashCounterDomain input)
        {
           List<GET_CashCounter> list = new List<GET_CashCounter>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CashCounter](@p0 , @p1 , @p2 , @p3)ORDER BY DATEO DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_CashCounter
                        {
                            PKID = reader.GetString("PKID"),
                            TOKENNO = reader.GetString("TOKENNO"),
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
                            ISCLOSE = reader.GetBoolean("ISCLOSE"),
                            BILLNO = reader.GetString("BILLNO"),
                            BAID = reader.GetString("BAID"),
                            FAID = reader.GetString("FAID"),
                            CHEQUENO = reader.GetString("CHEQUENO"),
                            DATEO = reader.GetDateTime("DATEO"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_CashCounter>> keyword(GET_CashCounterDomain input)
        {
            List<GET_CashCounter> list = new List<GET_CashCounter>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CashCounter](@p0 , @p1 , @p2 , @p3)ORDER BY DATEO DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.PKID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CashCounter
                        {
                            PKID = reader.GetString("PKID"),
                            TOKENNO = reader.GetString("TOKENNO"),
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
                            BILLNO = reader.GetString("BILLNO"),
                            BAID = reader.GetString("BAID"),
                            FAID = reader.GetString("FAID"),
                            DATEO = reader.GetDateTime("DATEO"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
