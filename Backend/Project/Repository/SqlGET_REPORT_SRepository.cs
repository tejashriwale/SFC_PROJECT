using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_REPORT_SRepository : IGET_REPORT_SRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_REPORT_SRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_REPORT_S>> getasync(GET_REPORT_SDomain input)
        {
            List<GET_REPORT_S> result = new List<GET_REPORT_S>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_REPORT_S]('" + input.@BAID + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' , '" + input.STARTDATE + "' , '" + input.ENDTDATE + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_REPORT_S
                        {

                            SEVA = reader.GetString("SEVA"),
                            SevaID = reader.GetString("SevaID"),
                            SevaValue = reader.GetString("SevaValue"),
                            nSERVICETYPETITLE = reader.GetString("nSERVICETYPETITLE"),
                            BILLNO = reader.GetInt32("BILLNO"),
                            BAID = reader.GetString("BAID"),




                        });
                    }
                }

            }
            return result;
        }
    }
}
