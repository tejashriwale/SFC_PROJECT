using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_VyapariTransectionReportRepository : IGET_VyapariTransectionReportRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_VyapariTransectionReportRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_VyapariTransectionReport>> getasync(GET_VyapariTransectionReportDomain input)
        {
            List<GET_VyapariTransectionReport> result = new List<GET_VyapariTransectionReport>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_VyapariTransectionReport]('" + input.VPAID + "' , '" + input.TRNDT + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_VyapariTransectionReport
                        {
                            TRNDT = reader.GetDateTime("TRNDT"),
                            VBILLNO = reader.GetString("VBILLNO"),
                            PERTICULERS = reader.GetString("PERTICULERS"),
                            TransactionType = reader.GetString("TransactionType"),
                            BILLAMOUNT = reader.GetDecimal("BILLAMOUNT"),
                            RECIVEAMOUNT = reader.GetDecimal("RECIVEAMOUNT"),
                            VDAID = reader.GetString("VDAID"),
                            VPAID = reader.GetString("VPAID"),
                            total = reader.GetDecimal("total")


                        });
                    }
                }
            }
            return result;
        }
    }
}
