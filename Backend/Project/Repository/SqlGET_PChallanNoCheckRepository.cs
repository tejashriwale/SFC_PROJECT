using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_PChallanNoCheckRepository : IGET_PChallanNoCheckRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_PChallanNoCheckRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_PChallanNoCheck>> getasync(GET_PChallanNoCheckDomain input)
        {
            List<GET_PChallanNoCheck> list = new List<GET_PChallanNoCheck>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT Top 20 * FROM [GET_PChallanNoCheck]('" + input.PCAID + "' ,'" + input.PCTRNNO + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_PChallanNoCheck
                        {
                           GRNAID = reader.GetString("GRNAID"),
                           GRNCHALLANNO = reader.GetString("GRNCHALLANNO"),
                           

                        });
                    }
                }
            }
            return list;
        }
    }
}
