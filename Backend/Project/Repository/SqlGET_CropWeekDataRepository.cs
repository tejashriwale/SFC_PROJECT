using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CropWeekDataRepository : IGET_CropWeekDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CropWeekDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CropWeekData>> getasync(GET_CropWeekDataDomain input)
        {
            List<GET_CropWeekData> result = new List<GET_CropWeekData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_CropWeekData]('" + input.startDate + "' , '" + input.endDate + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_CropWeekData
                        {
                            entry_date = reader.GetString("entry_date"),
                            day_name = reader.GetString("day_name"),
                            crop_label = reader.GetString("crop_label"),
                            total_carets = reader.GetInt32("total_carets"),
                           

                        });
                    }
                }
            }
            return result;
        }
    }
}
