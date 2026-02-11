using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CropMonthDataRepository : IGET_CropMonthDataRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CropMonthDataRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_CropMonthData>> getasync(GET_CropMonthDataDomain input)
        {
            List<GET_CropMonthData> result = new List<GET_CropMonthData>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CropMonthData]('" + input.month + "' , '" + input.year + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GET_CropMonthData
                        {
                            month_name = reader.GetString("month_name"),
                            month_value = reader.GetInt32("month_value"),
                            crop_label = reader.GetString("crop_label"),
                            monthly_count = reader.GetInt32("monthly_count"),
                        });
                    }
                }
            }
            return result;
        }
    }
}
