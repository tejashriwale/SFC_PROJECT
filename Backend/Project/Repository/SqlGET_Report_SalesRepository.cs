using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_Report_SalesRepository : IGET_Report_SalesRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_Report_SalesRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_Report_Sales>> getasync(GET_Report_SalesDomain input)
        {
            List<GET_Report_Sales> list = new List<GET_Report_Sales>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_Report_Sales]( @p0 ,@p1 , @p2,@p3 , @p4)ORDER BY SYSDT desc";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.CTAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.STARTDATE) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.ENDDATE) ?? ""); command.Parameters.Add(command_p4); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_Report_Sales
                        {
                            SYSDT = reader.GetString("SYSDT"),
                            TOTSAMT = reader.GetDouble("TOTSAMT"),
                            CROP = reader.GetString("CROP"),
                            CTRADENAME = reader.GetString("CTRADENAME"),
                            CTAID = reader.GetString("CTAID"),
                            TREADER = reader.GetString("TREADER"),
                            VEHCHNO = reader.GetString("VEHCHNO"),
                            CRTCNT = reader.GetInt32("CRTCNT"),
                            CROPTITLE = reader.GetString("CROPTITLE")



                        });
                    }
                }
            }
            return list;
        }
    }
}
