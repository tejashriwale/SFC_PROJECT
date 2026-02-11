using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_GateCashSummaryRepository : IGET_GateCashSummaryRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GateCashSummaryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_GateCashSummary>> getasync(GET_GateCashSummaryDomain input)
        {
            List<GET_GateCashSummary> list = new List<GET_GateCashSummary>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_GateCashSummary]( '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_GateCashSummary
                        {
                           
                            CASDATE = reader.GetString("CASDATE"),
                            CASBANKNAME = reader.GetString("CASBANKNAME"),
                            CASID = reader.GetString("CASID"),
                            CASINVERTCASH = reader.GetInt32("CASINVERTCASH"),
                            CAS2000 = reader.GetInt32("CAS2000"),
                            CAS500 = reader.GetInt32("CAS500"),
                            CAS200 = reader.GetInt32("CAS200"),
                            CAS100 = reader.GetInt32("CAS100"),
                            CAS10 = reader.GetInt32("CAS10"),
                            CAS50 = reader.GetInt32("CAS50"),
                            CAS20 = reader.GetInt32("CAS20"),
                            CASCOINS = reader.GetInt32("CASCOINS"),
                            CASTOTAL = reader.GetInt32("CASTOTAL"),
                            


                        });
                    }
                }
            }
            return list;
        }

      
        
    }
}
