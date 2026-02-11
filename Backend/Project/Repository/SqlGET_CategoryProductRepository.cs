using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlGET_CategoryProductRepository : IGET_CategoryProductRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlGET_CategoryProductRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_CategoryProduct>> getasync(GET_CategoryProductDomain customer)
        {
           List<GET_CategoryProduct> list = new List<GET_CategoryProduct>();
            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CategoryProduct]('"+customer.CTAID+"', '" + customer.COMPANYID + "', '" + customer.DEPTID + "')";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_CategoryProduct
                        {
                            PAID = reader.GetString("PAID"),
                            PNAME = reader.GetString("PNAME"),
                            HSNCODE = reader.GetInt32("HSNCODE"),
                            PSALES = reader.GetString("PSALES"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
