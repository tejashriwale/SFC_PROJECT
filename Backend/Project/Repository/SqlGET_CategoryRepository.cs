using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CategoryRepository : IGET_CategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlGET_CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_Category>> getasync(GET_CategoryDomain category)
        {
           List<GET_Category> list = new List<GET_Category>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_Category]('"+category.COMPANYID+"','" + category.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while (await  reader.ReadAsync())
                    {
                        list.Add(new GET_Category
                        {
                            CTAID = reader.GetString("CTAID"),
                            CTNAME = reader.GetString("CTNAME")
                            
                        });
                    }
                }
            }
            return list;
        }
    }
}
