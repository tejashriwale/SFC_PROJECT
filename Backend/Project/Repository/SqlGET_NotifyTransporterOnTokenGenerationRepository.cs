using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_NotifyTransporterOnTokenGenerationRepository : IGET_NotifyTransporterOnTokenGenerationRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_NotifyTransporterOnTokenGenerationRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_NotifyTransporterOnTokenGeneration>> getasync(GET_NotifyTransporterOnTokenGenerationDomain input)
        {
            List<GET_NotifyTransporterOnTokenGeneration> list = new List<GET_NotifyTransporterOnTokenGeneration>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_NotifyTransporterOnTokenGeneration]('" + input.DATE + "' , '" + input.VEHNO + "' , '" + input.TOKEN + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_NotifyTransporterOnTokenGeneration
                        {
                            FCMTOKEN = reader.GetString("FCMTOKEN"),
                            TAID = reader.GetString("TAID"),
                            TNAME = reader.GetString("TNAME"),
                            TOKNNO = reader.GetString("TOKNNO"),
                            VEHNO = reader.GetString("VEHNO"),
                            

                        });
                    }
                }
            }
            return list;
        }
    }
}
