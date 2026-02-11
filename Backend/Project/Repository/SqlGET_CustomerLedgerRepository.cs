using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CustomerLedgerRepository : IGET_CustomerLedgerRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CustomerLedgerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CustomerLedger>> getasync(GET_CustomerLedgerDomain input)
        {
            List<GET_CustomerLedger> list = new List<GET_CustomerLedger>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_CustomerLedger]('" + input.COMPANYID + "' ,  '" + input.DEPTID + "'   ,'" + input.REFAID + "','" + input.MAID + "','" + input.SDATE + "','" + input.EDATE + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CustomerLedger
                        {
                           MAID = reader.GetString("MAID"),
                           CBAL = reader.GetDouble("CBAL"),
                           REFAID = reader.GetString("REFAID"),
                           TOTSAMT = reader.GetDouble("TOTSAMT"),
                           PAMT = reader.GetDouble("PAMT"),
                           TRAID = reader.GetString("TRAID"),
                           TRNDT = reader.GetString("TRNDT"),
                            CCOMPANYNAME = reader.GetString("CCOMPANYNAME"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
