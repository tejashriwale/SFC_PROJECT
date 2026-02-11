using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_TotalPurchaseReportRepository : IGET_TotalPurchaseReportRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlGET_TotalPurchaseReportRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_TotalPurchaseReport>> getasync(GET_TotalPurchaseReportDomain input)
        {
            List<GET_TotalPurchaseReport> list = new List<GET_TotalPurchaseReport>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_TotalPurchaseReport]('" + input.CROPID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' , '" + input.STARTDATE + "' , '" + input.ENDDATE + "' )ORDER BY DATE DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_TotalPurchaseReport
                        {
                            CGRADE = reader.GetString("CGRADE"),
                            CROPINVERT = reader.GetString("CROPINVERT"),
                            CROPNAME = reader.GetString("CROPNAME"),
                            DATE = reader.GetString("DATE"),
                            Amount = reader.GetDouble("Amount"),
                            FAID = reader.GetString("FAID"),
                            FNAME = reader.GetString("FNAME"),
                            IMPLICATIONTitle = reader.GetString("IMPLICATIONTitle"),
                            RATE = reader.GetDouble("RATE"),
                            CTYPE = reader.GetString("CTYPE"),
                            VEHNO = reader.GetString("VEHNO"),
                            WEIGHT = reader.GetDouble("WEIGHT")
                        });
                    }
                }
            }
            return list;
        }
    }
}
