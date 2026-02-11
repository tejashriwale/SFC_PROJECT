using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_AdminVideoRepository : IGET_AdminVideoRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_AdminVideoRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_AdminVideo>> getasync(GET_AdminVideoDomain input)
        {
            List<GET_AdminVideo> list = new List<GET_AdminVideo>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_AdminVideo]('" + input.Vid + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "', '" + input.DEPTID + "')ORDER BY DATE DESC";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_AdminVideo
                        {
                            Vid = reader.GetString("Vid"),
                            ISACTIVE = reader.GetString("ISACTIVE"),
                            Video = reader.GetString("Video"),
                            Vtitle = reader.GetString("Vtitle"),
                        });
                    }
                }
            }
            return list;
        }

        public async Task<List<GET_AdminVideo>> getkeyword(GET_AdminVideoDomain input)
        {
            List<GET_AdminVideo> list = new List<GET_AdminVideo>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_AdminVideo]('" + input.Vid + "' , '" + input.KEYWORD + "' , '" + input.COMPANYID + "', '" + input.DEPTID + "')ORDER BY DATE DESC";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_AdminVideo
                        {
                            Vid = reader.GetString("Vid"),
                            ISACTIVE = reader.GetString("ISACTIVE"),
                            Video = reader.GetString("Video"),
                            Vtitle = reader.GetString("Vtitle"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
