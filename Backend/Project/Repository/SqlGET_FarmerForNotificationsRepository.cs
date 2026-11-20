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
                command.CommandText = "SELECT  * FROM [GET_FarmerForNotifications](@p0 , @p1, @p2)ORDER BY DATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.NotiID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
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
