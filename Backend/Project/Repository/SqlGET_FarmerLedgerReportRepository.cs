using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_FarmerLedgerReportRepository : IGET_FarmerLedgerReportRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FarmerLedgerReportRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_FarmerLedgerReport>> getasync(GET_FarmerLedgerReportDomain input)
        {
            List<GET_FarmerLedgerReport> list = new List<GET_FarmerLedgerReport>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_FarmerLedgerReport_Transactionwise](@p0, @p1, @p2, @p3) ORDER BY TRNSR ASC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.MAID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.CROPID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_FarmerLedgerReport
                        {
                            TRNDT = reader.GetString("TRNDT"),
                            BILLNO = reader.IsDBNull(reader.GetOrdinal("BILLNO")) ? "" : reader.GetString("BILLNO"),
                            FNAME = reader.GetString("FNAME"),
                            CROPNAME = reader.IsDBNull(reader.GetOrdinal("CROPNAME")) ? "" : reader.GetString("CROPNAME"),
                            CVARIETY = reader.IsDBNull(reader.GetOrdinal("CVARIETY")) ? "" : reader.GetString("CVARIETY"),
                            NARR = reader.IsDBNull(reader.GetOrdinal("NARR")) ? "" : reader.GetString("NARR"),
                            TRANSACTIONTYPE = reader.GetString("TRANSACTIONTYPE"),
                            DEBIT = reader.GetDouble("DEBIT"),
                            CREDIT = reader.GetDouble("CREDIT"),
                            BALANCE = reader.GetDouble("BALANCE"),
                            TRAID = reader.GetString("TRAID"),
                            TRNSR = reader.GetInt32("TRNSR"),
                            REFAID = reader.GetString("REFAID"),
                            FAID = reader.GetString("FAID"),
                            CROPID = reader.GetString("CROPID")
                        });
                    }
                }
            }
            return list;
        }
    }
}
