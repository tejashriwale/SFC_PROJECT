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
                command.CommandText = "SELECT  * FROM [GET_VyapariTransectionReport](@p0 , @p1 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.VPAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.TRNDT) ?? ""); command.Parameters.Add(command_p1); }
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
