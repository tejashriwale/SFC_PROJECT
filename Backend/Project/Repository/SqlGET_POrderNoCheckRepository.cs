using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_POrderNoCheckRepository : IGET_POrderNoCheckRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_POrderNoCheckRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_POrderNoCheck>> getasync(GET_POrderNoCheckDomain input)
        {
            List<GET_POrderNoCheck> list = new List<GET_POrderNoCheck>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_POrderNoCheck]('" + input.POAID + "' ,'" + input.POTRANNO + "' , '" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_POrderNoCheck
                        {
                          PCAID = reader.GetString("PCAID"),
                            PONO = reader.GetString("PONO"),


                        });
                    }
                }
            }
            return list;
        }
    }
}
