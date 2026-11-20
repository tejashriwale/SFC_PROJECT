using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_NotificationRepository : IGET_NotificationRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_NotificationRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_Notification>> getasync(GET_NotificationDomain input)
        {
            List<GET_Notification> list = new List<GET_Notification>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT TOP 20 * FROM [GET_Notification](@p0 , @p1 , @p2 , @p3)ORDER BY DATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.NotiID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_Notification
                        {
                            NotiID = reader.GetString("NotiID"),
                            NotiDescription = reader.GetString("NotiDescription"),
                            NotiEDate = reader.GetString("NotiEDate"),
                            NotiSDate = reader.GetString("NotiSDate"),
                            NotiTitle = reader.GetString("NotiTitle"),
                            ISACTIVE = reader.GetString("ISACTIVE"),
                            LOGTYPE = reader.GetInt32("LOGTYPE"),
                            DATE = reader.GetString("DATE"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_Notification>> getkeyword(GET_NotificationDomain input)
        {
            List<GET_Notification> list = new List<GET_Notification>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_Notification](@p0 , @p1 , @p2 , @p3)ORDER BY DATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.NotiID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.KEYWORD) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_Notification
                        {
                            NotiID = reader.GetString("NotiID"),
                            NotiDescription = reader.GetString("NotiDescription"),
                            NotiEDate = reader.GetString("NotiEDate"),
                            NotiSDate = reader.GetString("NotiSDate"),
                            NotiTitle = reader.GetString("NotiTitle"),
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
