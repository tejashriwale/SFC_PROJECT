using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_Report_SalesRepository : IGET_Report_SalesRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_Report_SalesRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_Report_Sales>> getasync(GET_Report_SalesDomain input)
        {
            List<GET_Report_Sales> list = new List<GET_Report_Sales>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_Report_Sales]( '" + input.CTAID + "' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "','" + input.STARTDATE + "' , '" + input.ENDDATE + "')ORDER BY SYSDT desc";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_Report_Sales
                        {
                            SYSDT = reader.GetString("SYSDT"),
                            TOTSAMT = reader.GetDouble("TOTSAMT"),
                            CROP = reader.GetString("CROP"),
                            CTRADENAME = reader.GetString("CTRADENAME"),
                            CTAID = reader.GetString("CTAID"),
                            TREADER = reader.GetString("TREADER"),
                            VEHCHNO = reader.GetString("VEHCHNO"),
                            CRTCNT = reader.GetInt32("CRTCNT"),
                            CROPTITLE = reader.GetString("CROPTITLE")



                        });
                    }
                }
            }
            return list;
        }
    }
}
