using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_MainGroupRepository : IGET_MainGroupRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_MainGroupRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_MainGroup>> getasync()
        {
           List<GET_MainGroup> list = new List<GET_MainGroup>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_MainGroup]()";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(reader.Read())
                    {
                        list.Add(new GET_MainGroup
                        {
                            ACCTM = reader.GetString("ACCTM"),
                            MGRPID = reader.GetString("MGRPID"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
