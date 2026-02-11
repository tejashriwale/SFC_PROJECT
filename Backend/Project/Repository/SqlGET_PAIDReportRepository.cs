using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_PAIDReportRepository : IGET_PAIDReportRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PAIDReportRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ;
        }
        public async Task<List<GET_PAIDReport>> GetAsync(GET_PAIDReportDomain input)
        {
            List<GET_PAIDReport> list = new List<GET_PAIDReport>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_PAIDReport]('" + input.STARTDATE + "' , '" + input.ENDTDATE + "' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_PAIDReport
                        {
                            DATE = reader.GetString("DATE"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            FNAME = reader.GetString("FNAME"),
                            VILLAGENAME = reader.GetString("VILLAGENAME"),
                            VEHICLENO = reader.GetString("VEHICLENO"),
                            TOTALCOST = reader.GetDouble("TOTALCOST"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            CASHAMOUNT = reader.GetDouble("CASHAMOUNT"),
                            CHEQUEAMOUNT = reader.GetDouble("CHEQUEAMOUNT"),
                            ONLINEAMOUNT = reader.GetDouble("ONLINEAMOUNT"),
                            TOTAL = reader.GetDouble("TOTAL"),
                            BAKIRAKKAM = reader.GetDouble("BAKIRAKKAM")


                        });
                    }
                }
            }
            return list;
        }
    }
}
