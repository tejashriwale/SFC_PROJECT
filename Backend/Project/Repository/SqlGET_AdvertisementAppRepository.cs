using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_AdvertisementAppRepository : IGET_AdvertisementAppRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_AdvertisementAppRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_AdvertisementApp>> GetAllAdvertisementsAsync()
        {
            List<GET_AdvertisementApp> list = new List<GET_AdvertisementApp>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_AdvertisementApp]()";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_AdvertisementApp
                        {
                            ADid = reader.GetString("ADid"),
                            ADtitle = reader.GetString("ADtitle"),
                            ADimage = reader.GetString("ADimage"),
                           


                        });
                    }
                }
            }
            return list;
        }
    }
}
