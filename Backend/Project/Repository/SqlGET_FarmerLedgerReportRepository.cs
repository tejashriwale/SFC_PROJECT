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
                command.CommandText = "SELECT * FROM [GET_FarmerLedgerReport_Transactionwise]('" +
                    input.COMPANYID + "', '" +
                    input.DEPTID + "', '" +
                    input.MAID + "', '" +
                    input.CROPID + "') ORDER BY TRNSR ASC";
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
