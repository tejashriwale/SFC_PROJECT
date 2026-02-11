using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_ShopNameRepository : IGET_ShopNameRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_ShopNameRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GET_ShopName>> getasync(GET_ShopNameDomain input)
        {
            List<GET_ShopName> list = new List<GET_ShopName>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_ShopName]('" + input.COMPANYID + "' , '" + input.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        list.Add(new GET_ShopName
                        {
                            STORENAME = reader.GetString("STORENAME"),
                            STORID = reader.GetString("STORID"),
                            STOREID = reader.GetString("STOREID"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
