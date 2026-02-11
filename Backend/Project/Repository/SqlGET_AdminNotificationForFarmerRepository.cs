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
                command.CommandText = "SELECT Top 20 * FROM [GET_AdminNotificationForFarmer]('" + input.NotiID + "' , '" + input.KEYWORD + "','" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
                command.CommandText = "SELECT  * FROM [GET_AdminNotificationForFarmer]('" + input.NotiID + "' , '" + input.KEYWORD + "','" + input.COMPANYID + "' , '" + input.DEPTID + "')";
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
