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
                commmand.CommandText = "SELECT * FROM [GET_VechicleProfitReport](@p0 ,@p1 ,@p2 ,@p3 )";
                { var commmand_p0 = commmand.CreateParameter(); commmand_p0.ParameterName = "@p0"; commmand_p0.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); commmand.Parameters.Add(commmand_p0); }
                { var commmand_p1 = commmand.CreateParameter(); commmand_p1.ParameterName = "@p1"; commmand_p1.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); commmand.Parameters.Add(commmand_p1); }
                { var commmand_p2 = commmand.CreateParameter(); commmand_p2.ParameterName = "@p2"; commmand_p2.Value = (object)(Convert.ToString(input.SDATE) ?? ""); commmand.Parameters.Add(commmand_p2); }
                { var commmand_p3 = commmand.CreateParameter(); commmand_p3.ParameterName = "@p3"; commmand_p3.Value = (object)(Convert.ToString(input.EDATE) ?? ""); commmand.Parameters.Add(commmand_p3); }
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
