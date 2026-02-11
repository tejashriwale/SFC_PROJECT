using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_StateCodeDistinctRepository : IGET_StateCodeDistinctRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlGET_StateCodeDistinctRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_StateCodeDistinct>> getasync()
        {
            List<GET_StateCodeDistinct> list = new List<GET_StateCodeDistinct>();
           using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_StateCodeDistinct]()";
                dbContext.Database.OpenConnection();

                using(var reader = await  command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_StateCodeDistinct
                        {
                            SSTATECODE = reader.GetString("SSTATECODE"),
                            SSTATENAME = reader.GetString("SSTATENAME")
                        });
                    }
                }
            }
           return list;
        }
    }
}
