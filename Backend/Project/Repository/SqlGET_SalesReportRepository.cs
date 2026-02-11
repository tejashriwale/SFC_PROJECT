using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_SalesReportRepository : IGET_SalesReportRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SalesReportRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SalesReport>> getasync(GET_SalesReportDomain input)
        {
            List<GET_SalesReport> list = new List<GET_SalesReport>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_SalesReport]('" + input.MAID + "' ,'" + input.SLVMID + "' ,  '" + input.COMPANYID + "'   ,'" + input.DEPTID + "','" + input.SDATE + "','" + input.EDATE + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SalesReport
                        {
                           CARETCOUNT = reader.GetString("CARETCOUNT"),
                           GRADE1 = reader.GetString("GRADE1"),
                           TOTSAMT = reader.GetDouble("TOTSAMT"),
                           TRADER1 = reader.GetString("TRADER1"),
                           CROP1 = reader.GetString("CROP1"),
                           SLVMID = reader.GetString("SLVMID"),
                           SYSDT = reader.GetString("SYSDT"),
                           VEHICLENO = reader.GetString("VEHICLENO"),
                            ITEMNE = reader.GetString("ITEMNE"),
                            CCOMPANYNAME = reader.GetString("CCOMPANYNAME"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
