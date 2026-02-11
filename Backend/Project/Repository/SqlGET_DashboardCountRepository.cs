using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_DashboardCountRepository : IGET_DashboardCountRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_DashboardCountRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_DashboardCount>> getasync(GET_DashboardCountDomain input)
        {
            List<GET_DashboardCount> list = new List<GET_DashboardCount>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_DashboardCount]('"+input.COMPANYID+"' , '"+input.DEPTID+"')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_DashboardCount
                        {
                            TotalJali = reader.GetInt32("TotalJali"),
                            TotalFarmer = reader.GetInt32("TotalFarmer"),
                        });
                    }
                }
            }
            return list;
        }
    
    }
}
