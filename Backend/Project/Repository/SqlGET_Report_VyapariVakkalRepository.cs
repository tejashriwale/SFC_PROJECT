using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_Report_VyapariVakkalRepository : IGET_Report_VyapariVakkalRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_Report_VyapariVakkalRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_Report_VyapariVakkal>> getasync(GET_Report_VyapariVakkalDomain input)
        {
            List<GET_Report_VyapariVakkal> list = new List<GET_Report_VyapariVakkal>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_Report_VyapariVakkal]('" + input.STARTDATE + "', '" + input.ENDTDATE + "' ,'" + input.COMPANYID + "' , '" + input.DEPTID + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_Report_VyapariVakkal
                        {
                            BILLNO = reader.GetInt32("BILLNO"),
                            WEIGHT = reader.GetString("WEIGHT"),
                            DATE = reader.GetString("DATE"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                            JAALI = reader.GetInt32("JAALI"),
                            RATE = reader.GetDouble("RATE"),
                            WEIGHTLESS = reader.GetDouble("WEIGHTLESS"),
                            VYAPARINAME = reader.GetString("VYAPARINAME"),
                            WEIGHTMAXMIN = reader.GetBoolean("WEIGHTMAXMIN")



                        });
                    }
                }
            }
            return list;
        }
    }
}
