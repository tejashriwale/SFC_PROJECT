using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Model.Domain;

namespace Project.Repository
{
    public class SqlIGET_CategoryCustomerRepository : IGET_CategoryCustomerRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlIGET_CategoryCustomerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GET_CategoryCustomer>> getasync(GET_CategoryCustomerDomain customer)
        {
            List<GET_CategoryCustomer> list = new List<GET_CategoryCustomer>();

            using(var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM [GET_CategoryCustomer]('"+customer.CTAID+ "','"+customer.CTYPE+"' , '" + customer.COMPANYID + "', '" + customer.DEPTID + "'  )";
                dbContext.Database.OpenConnection();

                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await  reader.ReadAsync())
                    {
                        list.Add(new GET_CategoryCustomer
                        {
                            CAID = reader.GetString("CAID"),
                            CCOMPANYNAME = reader.GetString("CCOMPANYNAME"),
                            CEMAIL = reader.GetString("CEMAIL"),
                            SSTATENAME = reader.GetString("SSTATENAME"),
                            CCONTACTPERSONMOBILE = reader.GetString("CCONTACTPERSONMOBILE"),
                        });
                    }
                }
            }
            return list;
        }
    }
}
