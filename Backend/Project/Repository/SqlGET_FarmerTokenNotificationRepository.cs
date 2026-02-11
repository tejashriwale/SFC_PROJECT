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
                command.CommandText = "SELECT * FROM[GET_FarmerTokenNotification]( '" + input.DATE + "' , '" + input.MAID + "','" + input.DPKID + "' , '" + input.VEHNO + "')";
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
