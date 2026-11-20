using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_SaveNotificationRepository : IGET_SaveNotificationRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SaveNotificationRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_SaveNotification>> getasync(GET_SaveNotificationDomain input)
        {
            List<GET_SaveNotification> list = new List<GET_SaveNotification>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_SaveNotification](@p0)ORDER BY DATE DESC";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.MAID) ?? ""); command.Parameters.Add(command_p0); }
                dbContext.Database.OpenConnection();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_SaveNotification
                        {
                            MAID = reader.GetString("MAID"),
                            NotiID = reader.GetString("NotiID"),
                            NotiTitle = reader.GetString("NotiTitle"),
                            NotiDescription = reader.GetString("NotiDescription"),
                            DATE = reader.GetString("DATE"),
                           
                        });
                    }
                }
            }
            return list;
        }
    }
    
    
}
