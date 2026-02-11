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
                command.CommandText = "SELECT TOP 20 * FROM [GET_Notification]('" + input.NotiID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY DATE DESC";
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
                command.CommandText = "SELECT * FROM [GET_Notification]('" + input.NotiID + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY DATE DESC";
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
