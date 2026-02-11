using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGetAdminAdvertisementRepository : IGetAdminAdvertisementRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGetAdminAdvertisementRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GetAdminAdvertisement>> getasync(GetAdminAdvertisementDomain input)
        {
            List<GetAdminAdvertisement> list = new List<GetAdminAdvertisement>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GetAdminAdvertisement]( '" + input.COMPANYID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GetAdminAdvertisement
                        {
                            ADid = reader.GetString("ADid"),
                            ADtitle = reader.GetString("ADtitle"),
                            ADimage = reader.GetString("ADimage"),
                            ISACTIVE = reader.GetString("ISACTIVE")
                        });
                    }
                }
            }
            return list;
        }
    }
}
