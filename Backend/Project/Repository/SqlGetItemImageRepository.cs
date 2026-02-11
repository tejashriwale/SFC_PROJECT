using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGetItemImageRepository : IGetItemImageRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGetItemImageRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GetItemImage>> getasync(GetItemImageDomain input)
        {
            List<GetItemImage> result = new List<GetItemImage>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GetItemImage]('" + input.COMPANYID + "')";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GetItemImage
                        {
                            ITEMAID = reader.GetString("ITEMAID"),
                            ITEMPHOTO = reader.GetString("ITEMPHOTO"),
                            ITEMNM = reader.GetString("ITEMNM"),
                            STATUS = reader.GetString("STATUS"),
                        });
                    }
                }
            }
            return result;
        }
    }
}
