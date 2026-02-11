using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_AdminNotificationForTransporterRepository : IGET_AdminNotificationForTransporterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_AdminNotificationForTransporterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_AdminNotificationForTransporter>> getasync(GET_AdminNotificationForTransporterDomain input)
        {
            List<GET_AdminNotificationForTransporter> list = new List<GET_AdminNotificationForTransporter>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_AdminNotificationForTransporter]('" + input.NotiID + "' , '" + input.KEYWORD + "','" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_AdminNotificationForTransporter
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

        public async Task<List<GET_AdminNotificationForTransporter>> getkeyword(GET_AdminNotificationForTransporterDomain input)
        {
            List<GET_AdminNotificationForTransporter> list = new List<GET_AdminNotificationForTransporter>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_AdminNotificationForTransporter]('" + input.NotiID + "' , '" + input.KEYWORD + "','" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_AdminNotificationForTransporter
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
