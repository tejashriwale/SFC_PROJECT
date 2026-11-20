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
                command.CommandText = "SELECT  * FROM [GET_Report_VyapariVakkal](@p0, @p1 ,@p2 , @p3 )";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.STARTDATE) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.ENDTDATE) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
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
