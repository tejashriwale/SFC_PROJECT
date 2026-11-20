using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_TransportersForNotificationsRepository : IGET_TransportersForNotificationsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_TransportersForNotificationsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_TransportersForNotifications>> getasync(GET_TransportersForNotificationsDomain input)
        {
            List<GET_TransportersForNotifications> list = new List<GET_TransportersForNotifications>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_TransportersForNotifications](@p0 , @p1, @p2 )ORDER BY TDATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.NotiID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_TransportersForNotifications
                        {
                            NotiID = reader.GetString("NotiID"),
                            TAID = reader.GetString("TAID"),
                            TNAME = reader.GetString("TNAME"), 
                            FCMTOKEN = reader.GetString("FCMTOKEN"),
                            LOGTYPE = reader.GetInt32("LOGTYPE"),
                            TDATE = reader.GetString("TDATE"),

                        });
                    }

                }
            }
            return list;
        }
    }
}
