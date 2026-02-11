using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_QuatationSRNoRepository : IGET_QuatationSRNoRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_QuatationSRNoRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_QuatationSRNo>> getasync(GET_QuatationSRNoDomain input)
        {
            List<GET_QuatationSRNo> list = new List<GET_QuatationSRNo>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_QuatationSRNo]('" + input.SPRNO + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_QuatationSRNo
                        {

                            SPRNO = reader.GetString("SPRNO"),
                          

                        });
                    }
                }
            }
            return list;
        }
    }
}
