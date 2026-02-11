using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_VechicleProfitReportRepository : IGET_VechicleProfitReportRepository
    {
        private ApplicationDbContext dbContext;
        public SqlGET_VechicleProfitReportRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_VechicleProfitReport>> getasync(GET_VechicleProfitReportDomain input)
        {
            List<GET_VechicleProfitReport> list = new List<GET_VechicleProfitReport>();
            using (var commmand = dbContext.Database.GetDbConnection().CreateCommand())
            {
                commmand.CommandText = "SELECT * FROM [GET_VechicleProfitReport]('" + input.COMPANYID + "' ,'" + input.DEPTID + "' ,'" + input.SDATE + "' ,'" + input.EDATE + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await commmand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_VechicleProfitReport
                        {
                            SYSDT = reader.GetString("SYSDT"),
                            CUSTOMERNAME = reader.GetString("CUSTOMERNAME"),
                            CROPNAME = reader.GetString("CROPNAME"),
                            TOTSAMT = reader.GetDouble("TOTSAMT"),
                            CROP = reader.GetString("CROP"),
                            CRTCNT = reader.GetInt32("CRTCNT"),
                            TOTEX = reader.GetDouble("TOTEX"),
                            TOTIWDAMT = reader.GetDouble("TOTIWDAMT"),
                            TREADER = reader.GetString("TREADER"),
                            VEHCHNO = reader.GetString("VEHCHNO")

                        });
                    }
                }
                return list;
            }
        }
    }
}
