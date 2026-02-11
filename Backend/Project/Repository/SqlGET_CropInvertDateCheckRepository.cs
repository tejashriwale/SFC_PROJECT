using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;
using System.Data;

namespace Project.Repository
{
    public class SqlGET_CropInvertDateCheckRepository : IGET_CropInvertDateCheckRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CropInvertDateCheckRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_CropInvertDateCheck>> getasync(GET_CropInvertDateCheckDomain input)
        {
            List<GET_CropInvertDateCheck> list = new List<GET_CropInvertDateCheck>();
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT  * FROM [GET_CropInvertDateCheck]('" + input.SHAID + "' , '" + input.SHSDATE + "' , '" + input.CGRADE + "' )";
                dbContext.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new GET_CropInvertDateCheck
                        {
                            DateExists = reader.GetInt32("DateExists"),
                         
                        });
                    }
                }
            }
            return list;
        }
    }
}
