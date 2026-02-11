using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_AdvertisementRepository : IGET_AdvertisementRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_AdvertisementRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_Advertisement>> getasync(GET_AdvertisementDomain input)
        {
            List<GET_Advertisement> list = new List<GET_Advertisement>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_Advertisement]('" + input.ADid + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY DATE DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_Advertisement
                        {
                            ADid = reader.GetString("ADid"),
                            ADimage = reader.GetString("ADimage"),
                            ISACTIVE = reader.GetString("ISACTIVE"),
                            ADtitle = reader.GetString("ADtitle"),
                            LOGTYPE = reader.GetString("LOGTYPE"),
                            DATE = reader.GetString("DATE"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_Advertisement>> getkeyword(GET_AdvertisementDomain input)
        {
            List<GET_Advertisement> list = new List<GET_Advertisement>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_Advertisement]('" + input.ADid + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')ORDER BY DATE DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_Advertisement
                        {
                            ADid = reader.GetString("ADid"),
                            ADimage = reader.GetString("ADimage"),
                            ISACTIVE = reader.GetString("ISACTIVE"),
                            ADtitle = reader.GetString("ADtitle"),
                            DATE = reader.GetString("DATE"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
