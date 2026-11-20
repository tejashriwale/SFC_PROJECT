using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_FarmerTokenNotificationRepository : IGET_FarmerTokenNotificationRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_FarmerTokenNotificationRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_FarmerTokenNotification>> getasync(GET_FarmerTokenNotificationDomain input)
        {
            List<GET_FarmerTokenNotification> list = new List<GET_FarmerTokenNotification>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM[GET_FarmerTokenNotification]( @p0 , @p1,@p2 , @p3)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(input.DATE) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(input.MAID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(input.DPKID) ?? ""); command.Parameters.Add(command_p2); }
                { var command_p3 = command.CreateParameter(); command_p3.ParameterName = "@p3"; command_p3.Value = (object)(Convert.ToString(input.VEHNO) ?? ""); command.Parameters.Add(command_p3); }
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_FarmerTokenNotification
                        {
                            TOKNNO = reader.GetInt32("TOKNNO"),
                            CARETS_COUNT = reader.GetInt32("CARETS_COUNT"),
                            VEHNO = reader.GetString("VEHNO"),
                            TAID = reader.GetString("TAID"),
                            TNAME = reader.GetString("TNAME"),
                            FAID = reader.GetString("FAID"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            FCMTOKEN = reader.GetString("FCMTOKEN"),
                            FARMERNAME = reader.GetString("FARMERNAME"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
