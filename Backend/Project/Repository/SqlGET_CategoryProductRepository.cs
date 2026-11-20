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
                command.CommandText = "SELECT * FROM [GET_CategoryProduct](@p0, @p1, @p2)";
                { var command_p0 = command.CreateParameter(); command_p0.ParameterName = "@p0"; command_p0.Value = (object)(Convert.ToString(customer.CTAID) ?? ""); command.Parameters.Add(command_p0); }
                { var command_p1 = command.CreateParameter(); command_p1.ParameterName = "@p1"; command_p1.Value = (object)(Convert.ToString(customer.COMPANYID) ?? ""); command.Parameters.Add(command_p1); }
                { var command_p2 = command.CreateParameter(); command_p2.ParameterName = "@p2"; command_p2.Value = (object)(Convert.ToString(customer.DEPTID) ?? ""); command.Parameters.Add(command_p2); }
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
