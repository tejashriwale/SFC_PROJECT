using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_GenrateACCNORepository : IGET_GenrateACCNORepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_GenrateACCNORepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_GenrateACCNO>> GetAll()
        {
            List<GET_GenrateACCNO> list = new List<GET_GenrateACCNO>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_GenrateACCNO]()";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_GenrateACCNO
                        {
                            CNT = reader.GetInt32("CNT"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
