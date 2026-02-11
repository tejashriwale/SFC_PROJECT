using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlHSNGSTDetailsRepository : IHSNGSTDetailsRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlHSNGSTDetailsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<HSNGSTDetails>> getasync(HSNDOMAIN hSNDOMAIN)
        {
            List<HSNGSTDetails> list = new List<HSNGSTDetails>();

            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_HSNGSTDetails]('"+ hSNDOMAIN.HAID  +"')";

                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await  reader.ReadAsync())
                    {
                        list.Add(new HSNGSTDetails
                        {
                            HIGST = reader.GetDouble("HIGST"),
                            HSGST = reader.GetDouble("HSGST"),
                            HCGST = reader.GetDouble("HCGST"),
                            HCESS = reader.GetDouble("HCESS"),
                        });
                    }
                }

            }
            return list;
        }
    }
}
