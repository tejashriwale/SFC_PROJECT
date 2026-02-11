using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGetVyapriDetailsReportRepository : IGetVyapriDetailsReportRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGetVyapriDetailsReportRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GetVyapriDetailsReport>> getasync(GetVyapriDetailsReportDomain input)
        {
            List<GetVyapriDetailsReport> list = new List<GetVyapriDetailsReport>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GetVyapriDetailsReport]('" + input.STARTDATE + "' , '" + input.ENDTDATE + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GetVyapriDetailsReport
                        {
                            BILLNO = reader.GetInt32("BILLNO"),
                            DATE = reader.GetString("DATE"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            VBKID = reader.GetString("VBKID"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            TOTALWEIGHT = reader.GetDouble("TOTALWEIGHT"),
                            TOTALLOSS = reader.GetDouble("TOTALLOSS"),
                            TOTALJAALI = reader.GetInt32("TOTALJAALI")
                        });
                    }
                }
            }
            return list;
        }
    }
}
