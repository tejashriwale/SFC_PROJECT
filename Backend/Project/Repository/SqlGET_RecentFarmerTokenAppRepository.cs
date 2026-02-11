using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_RecentFarmerTokenAppRepository : IGET_RecentFarmerTokenAppRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_RecentFarmerTokenAppRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_RecentFarmerTokenApp>> getasync(GET_RecentFarmerTokenAppDomain input)
        {
            List<GET_RecentFarmerTokenApp> list = new List<GET_RecentFarmerTokenApp>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_RecentFarmerTokenApp]('" + input.VEHNO + "' , '" + input.DATE + "' , '" + input.AADHARNO + "', '" + input.MOBILENO + "' , '" + input.MAID + "' , '"+input.CROP+"')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_RecentFarmerTokenApp
                        {
                            CCNT= reader.GetString("CCNT"),
                            TOKEN= reader.GetString("TOKEN"),
                            DPKID = reader.GetString("DPKID"),
                           

                        });
                    }
                }
            }
            return list;
        }
    }
    
}
