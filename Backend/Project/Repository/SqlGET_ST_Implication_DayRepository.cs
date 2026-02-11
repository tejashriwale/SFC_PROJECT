using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_ST_Implication_DayRepository : IGET_ST_Implication_DayRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ST_Implication_DayRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_ST_Implication_Day>> GetAll()
        {
            List<GET_ST_Implication_Day> list = new List<GET_ST_Implication_Day>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_ST_Implication_Day]()";
                dbContext.Database.OpenConnection();
                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_ST_Implication_Day
                        {
                            CNT = reader.GetInt32("CNT"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
