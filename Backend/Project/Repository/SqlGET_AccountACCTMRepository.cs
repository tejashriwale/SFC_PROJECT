using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_AccountACCTMRepository : IGET_AccountACCTMRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_AccountACCTMRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_AccountACCTM>> GetAll()
        {
            List<GET_AccountACCTM> list = new List<GET_AccountACCTM>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_AccountACCTM]()";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_AccountACCTM
                        {
                            ACCTM = reader.GetString("ACCTM"),
                            SGLRPID = reader.GetString("SGLRPID"),
                            RELID = reader.GetString("RELID"),
                        });
                    }
                }
   
            }
            return list;
        }
    }
}
