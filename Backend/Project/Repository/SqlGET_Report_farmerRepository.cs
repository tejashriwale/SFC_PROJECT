using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_Report_farmerRepository : IGET_Report_farmerRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_Report_farmerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_Report_farmer>> getasync(GET_Report_farmerDomain input)
        {
            List<GET_Report_farmer> result = new List<GET_Report_farmer>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_Report_farmer]('" + input.STARTDATE + "' , '" + input.ENDTDATE + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_Report_farmer
                        {

                            AMOUNT = reader.GetDouble("AMOUNT"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            DATE = reader.GetString("DATE"),
                            JALI = reader.GetInt32("JALI"),
                            RATE = reader.GetDouble("RATE"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            TOTALCARETS = reader.GetDouble("TOTALCARETS"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            CROP_TYPE = reader.GetString("CROP_TYPE"),
                            CROPLABEL = reader.GetString("CROPLABEL"),
                            TOTALCOST = reader.GetDouble("TOTALCOST"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            WEIGHTMINMAX = reader.GetString("WEIGHTMINMAX"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                            TOTALWEIGHT = reader.GetDouble("TOTALWEIGHT"),
                         
                            BTOKANNO = reader.GetString("BTOKANNO"),
                            VILLAGENAME = reader.GetString("VILLAGENAME"),
                            BAID = reader.GetString("BAID"),
                        });
                    }
                }

            }
            return result;
        }
    }
}
