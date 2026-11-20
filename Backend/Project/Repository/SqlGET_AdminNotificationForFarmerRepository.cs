using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_AdminNotificationForFarmerRepository : IGET_AdminNotificationForFarmerRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_AdminNotificationForFarmerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_AdminNotificationForFarmer>> getasync(GET_AdminNotificationForFarmerDomain input)
        {
            List<GET_AdminNotificationForFarmer> list = new List<GET_AdminNotificationForFarmer>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_AdminNotificationForFarmer](@p0 , @p1,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.NotiID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_AdminNotificationForFarmer
                        {
                            NotiID = reader.GetString("NotiID"),
                            NotiTitle = reader.GetString("NotiTitle"),
                            NotiDescription = reader.GetString("NotiDescription"),
                            NotiSDate = reader.GetString("NotiSDate"),
                            NotiEDate = reader.GetString("NotiEDate"),
                            ISACTIVE = reader.GetString("ISACTIVE"),
                            LOGTYPE = reader.GetInt32("LOGTYPE"),
                             DATE = reader.GetString("DATE"),
                           
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_AdminNotificationForFarmer>> getkeyword(GET_AdminNotificationForFarmerDomain input)
        {
            List<GET_AdminNotificationForFarmer> list = new List<GET_AdminNotificationForFarmer>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_AdminNotificationForFarmer](@p0 , @p1,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.NotiID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_AdminNotificationForFarmer
                        {
                            NotiID = reader.GetString("NotiID"),
                            NotiTitle = reader.GetString("NotiTitle"),
                            NotiDescription = reader.GetString("NotiDescription"),
                            NotiSDate = reader.GetString("NotiSDate"),
                            NotiEDate = reader.GetString("NotiEDate"),
                            ISACTIVE = reader.GetString("ISACTIVE"),
                            LOGTYPE = reader.GetInt32("LOGTYPE"),
                            DATE = reader.GetString("DATE"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
