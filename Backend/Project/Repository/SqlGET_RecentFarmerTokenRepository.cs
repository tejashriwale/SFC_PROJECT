using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_RecentFarmerTokenRepository : IGET_RecentFarmerTokenRepository
    {
       private readonly ApplicationDbContext dbContext;
        public SqlGET_RecentFarmerTokenRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_RecentFarmerToken>> getasync(GET_RecentFarmerTokenDomain input)
        {
            List<GET_RecentFarmerToken> list = new List<GET_RecentFarmerToken>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_RecentFarmerToken]('" + input.VEHNO + "', '" + input.DATE + "' ,'" + input.AADHARNO + "' , '" + input.MOBILENO + "' , '"+input.MAID+"' , '"+input.CROP+"')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_RecentFarmerToken
                        {
                            CCNT = reader.GetString("CCNT"),
                            TOKEN = reader.GetString("TOKEN"),
                            DPKID = reader.GetString("DPKID"),

                        });
                    }
                }
            }
            return list;
        }
    }
}
