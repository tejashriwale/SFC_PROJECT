using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_SubgroupRepository : IGET_SubgroupRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_SubgroupRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_Subgroup>> getasync(GET_SubgroupDomain input)
        {
            List<GET_Subgroup> list = new List<GET_Subgroup>();
            using(var commmand = dbContext.Database.GetDbConnection().CreateCommand())
            {
                commmand.CommandText = "SELECT * FROM [GET_Subgroup]('" + input.MGRPID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await commmand.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_Subgroup
                        {
                            ACCTM = reader.GetString("ACCTM"),
                            SGRPID = reader.GetString("SGRPID"),
                        });
                    }
                }
                return list;
            }
        }
    }
}
