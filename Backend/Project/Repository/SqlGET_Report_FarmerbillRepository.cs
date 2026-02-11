using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_Report_FarmerbillRepository : IGET_Report_FarmerbillRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_Report_FarmerbillRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_Report_Farmerbill>> getasync(GET_Report_FarmerbillDomain input)
        {
            List<GET_Report_Farmerbill> list = new List<GET_Report_Farmerbill>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_Report_Farmerbill]('" + input.STARTDATE + "','" + input.ENDTDATE + "','" + input.COMPANYID + "', '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_Report_Farmerbill
                        {
                            TOKENNO = reader.GetString("TOKENNO"),
                            BILLNO = reader.GetString("BILLNO"),
                            FULLNAME = reader.GetString("FULLNAME"),
                            BILLAMOUNT = reader.GetDouble("BILLAMOUNT"),
                            CASHAMOUNT = reader.GetDouble("CASHAMOUNT"),
                            CHEQUEAMOUNT = reader.GetDouble("CHEQUEAMOUNT"),
                            ONLINEAMOUNT = reader.GetDouble("ONLINEAMOUNT"),
                         
                          

                        });
                    }
                }
            }
            return list;
        }
    }
}
