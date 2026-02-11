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
                command.CommandText = "SELECT  * FROM [GET_TransportersForNotifications]('"+input.NotiID+"' , '"+input.COMPANYID+"', '"+input.DEPTID+"' )ORDER BY TDATE DESC";
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
