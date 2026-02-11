using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using Project.Model.DTO;

namespace Project.Repository
{
    public class SqlGET_DashCountRepository : IGET_DashCountRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_DashCountRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_DashCount>> getasync( )
        {
           List<GET_DashCount> list = new List<GET_DashCount>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_DashCount]( )";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_DashCount
                        {
                            TransporterCount = reader.GetInt32("TransporterCount"),
                            FarmerCount = reader.GetInt32("FarmerCount"),
                            VyapariCount = reader.GetInt32("VyapariCount"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
