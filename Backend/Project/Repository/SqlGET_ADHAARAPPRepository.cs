using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_ADHAARAPPRepository : IGET_ADHAARAPPRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ADHAARAPPRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_ADHAARAPP>> getasync(GET_ADHAARAPPDomain input)
        {
           List<GET_ADHAARAPP> list = new List<GET_ADHAARAPP>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_ADHAARAPP]('" + input.FADDHARNO + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_ADHAARAPP
                        {
                            FAID = reader.GetString("FAID"),
                            FNAME = reader.GetString("FNAME"),
                            FADDHARNO = reader.GetString("FADDHARNO"),
                            FBIRTHDATE = reader.GetString("FBIRTHDATE"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
