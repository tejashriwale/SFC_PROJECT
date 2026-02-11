using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_FarmerForNotificationsRepository : IGET_FarmerForNotificationsRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FarmerForNotificationsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_FarmerForNotifications>> getasync(GET_FarmerForNotificationsDomain input)
        {
            List<GET_FarmerForNotifications> list = new List<GET_FarmerForNotifications>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_FarmerForNotifications]('"+input.NotiID+"' , '"+input.COMPANYID+"', '"+input.DEPTID+"')ORDER BY DATE DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_FarmerForNotifications
                        {
                            FAID = reader.GetString("FAID"),
                            LOGTYPE = reader.GetInt32("LOGTYPE"),
                            FNAME = reader.GetString("FNAME"),
                            FCMTOKEN = reader.GetString("FCMTOKEN"),
                            DATE = reader.GetString("DATE"),
                        
                        });
                    }

                }
            }
            return list;
        }
    }
}
