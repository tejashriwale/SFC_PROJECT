using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_Report_VyapariBillRepository : IGET_Report_VyapariBillRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_Report_VyapariBillRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_Report_VyapariBill>> getasync(GET_Report_VyapariBillDomain input)
        {
            List<GET_Report_VyapariBill> result = new List<GET_Report_VyapariBill>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_Report_VyapariBill]('" + input.STARTDATE + "' , '" + input.ENDTDATE + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_Report_VyapariBill
                        {

                           
                            BILLNO = reader.GetInt32("BILLNO"),
                            DATE = reader.GetString("DATE"),
                            VNAME = reader.GetString("VNAME"),
                            RATE = reader.GetDouble("RATE"),
                            REMAININGAMOUNT = reader.GetDouble("REMAININGAMOUNT"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            LIMITS = reader.GetBoolean("LIMITS"),
                            PHONE = reader.GetString("PHONE"),
                            WEIGHT = reader.GetDouble("WEIGHT"),
                            TOTALAMOUNT = reader.GetDouble("TOTALAMOUNT"),
                            TOTALWEIGHT = reader.GetDouble("TOTALWEIGHT"),

                        });
                    }
                }

            }
            return result;
        }
    }
}
