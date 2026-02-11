using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_GateCashSummaryCashCountRepository : IGET_GateCashSummaryCashCountRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GateCashSummaryCashCountRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_GateCashSummaryCashCount>> getasync(GET_GateCashSummaryCashCountDomain input)
        {
            List<GET_GateCashSummaryCashCount> result = new List<GET_GateCashSummaryCashCount>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_GateCashSummaryCashCount]('" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_GateCashSummaryCashCount
                        {
                            CASDATE = reader.GetString("CASDATE"),
                            CASBANKNAME = reader.GetString("CASBANKNAME"),
                            CASINVERTCASH = reader.GetInt32("CASINVERTCASH"),
                            CASID = reader.GetString("CASID"),
                            CNT2000 = reader.GetInt32("CNT2000"),
                            CNT500 = reader.GetInt32("CNT500"),
                            CNT200 = reader.GetInt32("CNT200"),
                            CNT100 = reader.GetInt32("CNT100"),
                            CNT50 = reader.GetInt32("CNT50"),
                            CNT20 = reader.GetInt32("CNT20"),
                            CNT10 = reader.GetInt32("CNT10"),
                         
                            CNTCOINS = reader.GetInt32("CNTCOINS"),

                        });
                    }
                }
            }
            return result;
        }
    }
}
