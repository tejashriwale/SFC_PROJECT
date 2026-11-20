using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_TotalPurchaseReportRepository : IGET_TotalPurchaseReportRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlGET_TotalPurchaseReportRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_TotalPurchaseReport>> getasync(GET_TotalPurchaseReportDomain input)
        {
            List<GET_TotalPurchaseReport> list = new List<GET_TotalPurchaseReport>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_TotalPurchaseReport](@p0 , @p1 , @p2 , @p3 , @p4 )ORDER BY DATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.CROPID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.STARTDATE) ?? ""); command.Parameters.Add(command_p3); }
                { var command_p4 = command.CreateParameter(); command_p4.ParameterName = "@p4"; command_p4.Value = (object)(Convert.ToString(input.ENDDATE) ?? ""); command.Parameters.Add(command_p4); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_TotalPurchaseReport
                        {
                            CGRADE = reader.GetString("CGRADE"),
                            CROPINVERT = reader.GetString("CROPINVERT"),
                            CROPNAME = reader.GetString("CROPNAME"),
                            DATE = reader.GetString("DATE"),
                            Amount = reader.GetDouble("Amount"),
                            FAID = reader.GetString("FAID"),
                            FNAME = reader.GetString("FNAME"),
                            IMPLICATIONTitle = reader.GetString("IMPLICATIONTitle"),
                            RATE = reader.GetDouble("RATE"),
                            CTYPE = reader.GetString("CTYPE"),
                            VEHNO = reader.GetString("VEHNO"),
                            WEIGHT = reader.GetDouble("WEIGHT")
                        });
                    }
                }
            }
            return list;
        }
    }
}
